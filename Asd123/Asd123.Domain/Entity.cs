using System;
using System.Collections.Generic;
using System.Text;

namespace Asd123.Domain
{
    public abstract class Entity : IEntity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.Now;
            UpdatedAt = DateTimeOffset.Now;
        }

        public Guid Id { get; internal set; }

        public DateTimeOffset CreatedAt { get; internal set; }

        public DateTimeOffset UpdatedAt { get; internal set; }
    }
}
