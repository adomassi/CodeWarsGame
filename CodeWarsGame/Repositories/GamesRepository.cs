using System;
using System.Collections.Generic;
using System.Linq;
using CodeWarsGame.Models;

namespace CodeWarsGame.Repositories
{
    public class GamesRepository : IDisposable
    {
        private readonly CodeWarsGameContext _db;

        public GamesRepository()
        {
            _db = new CodeWarsGameContext();
        }

        public List<Scores> GetAllGames()
        {
            var successfullTasks = _db.SolvedTasks.GroupBy(x => x.Player.PlayerName).ToList();
            var scores = new List<Scores>();

            foreach (var item in successfullTasks)
            {
                scores.Add(Scores.Create(item.Key, item.Count()));
            }

            return scores;
        }

        public IEnumerable<string> GetSuccessfullTaskNames(string playerName)
        {
            return _db.SolvedTasks.Where(x => x.Player.PlayerName == playerName).OrderBy(x => x.Id).Select(x=> x.Task.TaskName).ToList();
        }

        public void AddSubmissionToScores(Player player, int taskId)
        {           
            var found = _db.Players.FirstOrDefault(x => x.PlayerName == player.PlayerName);

            var solvedTask = new SolvedTask { TaskId = taskId };
            found?.SolvedTasks.Add(solvedTask);

            if (found == null)
            {
                player.SolvedTasks.Add(solvedTask);
                _db.Players.Add(player);
            }

            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}