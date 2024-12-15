using MathGame.Models;
using SQLite;
using System.ComponentModel;

namespace MathGame.Data
{
    public class GameRepository
    {
        string _dbPath;
        private SQLiteConnection _db;
        public GameRepository(string dbPath)
        {
            _dbPath = dbPath;
            /*_db = new SQLiteConnection(
                @"c:\temp\mathgame.db",
                SQLiteOpenFlags.Create |
                SQLiteOpenFlags.FullMutex |
                SQLiteOpenFlags.ReadWrite);*/
        }

        public void Initialize()
        {
            _db = new SQLiteConnection(_dbPath);
            _db.CreateTable<Game>();
        }

        public List<Game> GetGames()
        {
            Initialize();
            return _db.Table<Game>().ToList();
        }

        public void AddGame(Game game)
        {
            Initialize();
            _db = new SQLiteConnection(_dbPath);
            _db.Insert(game);
        }

        public void DeleteGame(int id)
        {
            _db = new SQLiteConnection(_dbPath);
            _db.Delete(new Game { Id = id });
        }
    }
}
