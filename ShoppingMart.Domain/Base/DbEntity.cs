using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingMart.Domain.Base
{
    public abstract class DbEntity<T>
    {

        [Timestamp]
        [JsonIgnore]
        public byte[] RowVersion { get; set; }

        public T Id { get; set;}
        [JsonIgnore]
        public bool Deleted { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public DbEntity()
        {
            DateCreated = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
            Deleted = false;
        }
    }

    public abstract class DbGuidEntity : DbEntity<Guid>
    {
        public DbGuidEntity() : base()
        {
            Id = Guid.NewGuid();
        }
    }
}
