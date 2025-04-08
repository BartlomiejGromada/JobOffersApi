using System;
using System.Collections.Generic;

namespace JobOffersApi.Abstractions.Core;

public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : struct
{
    public TId Id { get; protected set; }

    protected Entity()
    {    
    }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity<TId> entity)
        {
            return false;
        }

        return EqualityComparer<TId>.Default.Equals(entity.Id, Id);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id) * 41;
    }

    public bool Equals(Entity<TId>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return EqualityComparer<TId>.Default.Equals(other.Id, Id);
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return (left is not null && right is not null && left.Equals(right));
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !(left == right);
    }
}
