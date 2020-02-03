using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationHudl.Model
{
    public class OpponentsItem
    {
        public int GameId { get; set; }

        public int SqlId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Opponent { get; set; }

        [Required]
        public int OpponentId { get; set; }

        [Required]
        public bool IsHome { get; set; }

        [Required]
        public int GameType { get; set; }

        public string[] Categories { get; set; }
    }
}
