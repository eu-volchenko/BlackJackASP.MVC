using Dapper.Contrib.Extensions;

namespace BlackJackDAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class History:BaseEntity
    {
        public DateTime LogDateTime { get; set; }

        public int GameId { get; set; }

        [Computed]
        public virtual Game Game { get; set; }

        [Computed]
        public virtual BaseEntity BaseEntity { get; set; }
    }
}
