using System;

namespace Rm.Core.Entity
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public DateTime? UpdatedDateTime { get; set; }
    }
}
