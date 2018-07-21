using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeWarsGame.Models
{
    public sealed class Player
    {
        public Player()
        {
            SolvedTasks = new HashSet<SolvedTask>();
        }

        [Key]
        public int PlayerId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(450)]
        [Index(IsUnique = true)]
        [DisplayName("Nickname")]
        public string PlayerName { get; set; }
        public ICollection<SolvedTask> SolvedTasks { get; set; } 

    }
}