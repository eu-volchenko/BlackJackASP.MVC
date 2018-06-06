using Dapper.Contrib.Extensions;

namespace BlackJackDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserCard:BaseEntity
    {
        public int UserId { get; set; }

        public int CardId { get; set; }

        public int RoundId { get; set; }

        [Computed]
        public virtual Card Card { get; set; }

        [Computed]
        public virtual Round Round { get; set; }

        [Computed]
        public virtual User User { get; set; }
        [Computed]
        public virtual BaseEntity BaseEntity { get; set; }

    }
}
