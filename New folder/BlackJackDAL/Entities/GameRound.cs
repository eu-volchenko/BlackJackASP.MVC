namespace BlackJackDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GameRound
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int RoundId { get; set; }

        public virtual Game Game { get; set; }

        public virtual Round Round { get; set; }
    }
}
