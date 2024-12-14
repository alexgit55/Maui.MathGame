namespace MathGame
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            // Change the window Size
            window.Width = 1000; window.Height = 800;

            // BONUS -> Center the window
            var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
            window.X = (displayInfo.Width / displayInfo.Density - window.Width) / 2;
            window.Y = (displayInfo.Height / displayInfo.Density - window.Height) / 2;
            return window;
        }
    }
}
