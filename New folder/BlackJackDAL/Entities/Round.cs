namespace BlackJackDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Round
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Round()
        {
            GameRounds = new HashSet<GameRound>();
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public int UserCardId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameRound> GameRounds { get; set; }

        public virtual User User { get; set; }

        public virtual UserCard UserCard { get; set; }
    }
}
