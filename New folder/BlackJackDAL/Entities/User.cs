using Dapper.Contrib.Extensions;

namespace BlackJackDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User:BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Rounds = new HashSet<Round>();
            UserCards = new HashSet<UserCard>();
        }
        [Required]
        public string Name { get; set; }

        public int TypeId { get; set; }

        public int GameId { get; set; }

        [Computed]
        public virtual Game Game { get; set; }

        [Computed]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Round> Rounds { get; set; }

        [Computed]
        public virtual Type Type { get; set; }

        [Computed]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCard> UserCards { get; set; }
        [Computed]
        public virtual BaseEntity BaseEntity { get; set; }

    }
}
