using MathGame.Models;

namespace MathGame;

public partial class GamePage : ContentPage
{
    public string GameType { get; set; }

    int firstNumber = 0;
    int secondNumber = 0;
    int score = 0;
    const int totalQuestions = 10;
    int questionNumber = 1;

    public GamePage(string gameType)
    {
        InitializeComponent();
        GameType = gameType;
        BindingContext = this;

        CreateNewQuestion();

    }

    private void CreateNewQuestion()
    {
        var gameOperand = GameType switch
        {
            "Addition" => "+",
            "Subtraction" => "-",
            "Multiplication" => "*",
            "Division" => "/",
            _ => throw new InvalidOperationException("Invalid game type")
        };

        // Generate two random numbers
        firstNumber = GameType != "Division" ? new Random().Next(1, 10) : new Random().Next(2, 100);
        secondNumber = GameType != "Division" ? new Random().Next(1, 10) : new Random().Next(2, 100);

        //Check if division will have a remainder
        if (GameType == "Division")
        {
            while (firstNumber == secondNumber || firstNumber % secondNumber != 0)
            {
                firstNumber = new Random().Next(2, 100);
                secondNumber = new Random().Next(2, 100);
            }
        }

        // Display the question
        QuestionCount.Text = $"Question {questionNumber} of {totalQuestions}";
        QuestionLabel.Text = $"{firstNumber} {gameOperand} {secondNumber} = ?";
    }

    private void OnAnswerSubmitted(object sender, EventArgs e)
    {
        var answer = int.Parse(AnswerEntry.Text);
        var iscorrect = false;

        switch (GameType)
        {
            case "Addition":
                iscorrect = answer == firstNumber + secondNumber;
                break;
            case "Subtraction":
                iscorrect = answer == firstNumber - secondNumber;
                break;
            case "Multiplication":
                iscorrect = answer == firstNumber * secondNumber;
                break;
            case "Division":
                iscorrect = answer == firstNumber / secondNumber;
                break;
        }

        ProcessAnswer(iscorrect);
        AnswerEntry.Text = string.Empty;
        questionNumber++;

        if (questionNumber <= totalQuestions)
            CreateNewQuestion();
        else
            GameOver();

    }

    private void GameOver()
    {
        GameOperation gameType = GameType switch
        {
            "Addition" => GameOperation.Addition,
            "Subtraction" => GameOperation.Subtraction,
            "Multiplication" => GameOperation.Multiplication,
            "Division" => GameOperation.Division,
            _ => GameOperation.Random
        };

        GameOverLabel.Text = $"Game Over! You scored {score} out of {totalQuestions}";
        QuestionArea.IsVisible = false;
        BackToMain.IsVisible = true;

        App.GameRepository.AddGame(new Game
        {
            Type = gameType,
            Score = score,
            DatePlayed = DateTime.Now
        });
    }

    private void ProcessAnswer(bool iscorrect)
    {
        if (iscorrect)
            score++;

        ResultLabel.Text = iscorrect ? "Correct!" : "Incorrect";
    }

    private void OnBackToMain(object sender, EventArgs e)
    {
        score = 0;
        questionNumber = 1;
        Navigation.PushAsync(new MainPage());
    }
}