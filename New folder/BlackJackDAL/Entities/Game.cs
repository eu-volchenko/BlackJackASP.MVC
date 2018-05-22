namespace BlackJackDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Game
    {
        public Game()
        {
            //GameRounds = new HashSet<GameRound>();
            //Histories = new HashSet<History>();
        }
        
        public int Id { get; set; }

        public int NumberOfPlayers { get; set; }

        //public virtual ICollection<GameRound> GameRounds { get; set; }
        //public virtual IEnumerable<GameRound> GameRounds { get; set; }

        //public virtual IEnumerable<History> Histories { get; set; }
        //public virtual ICollection<History> Histories { get; set; }
    }
}
