namespace BlackJackDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class History
    {
        public int Id { get; set; }

        public DateTime LogDateTime { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }
    }
}
