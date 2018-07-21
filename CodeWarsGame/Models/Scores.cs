using System.Collections.Generic;
using System.ComponentModel;

namespace CodeWarsGame.Models
{
    public sealed class Scores
    {
        [DisplayName("Nickname")]
        public string PlayerName { get; private set; }
        [DisplayName("Total successfull tasks")]
        public int TotalSuccessfullTasks { get; private set; }
       

        public static Scores Create(string playerName, int totalSuccessfullTasks)
        {
            return new Scores
            {
                PlayerName = playerName,
                TotalSuccessfullTasks = totalSuccessfullTasks
            };
        }
    }
}