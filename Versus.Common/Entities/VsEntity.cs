using System;
using System.Collections.Generic;
using Versus.Common.Base;

namespace Versus.Common.Entities
{
    public class VsEntity : IVsEntity
    {
        public string Name { get; set; }
        public int Id { get; private set; }

        public VsEntity()
        {
            Id = new Guid().GetHashCode();
        }

        public string GetEntityName()
        {
            return Name;
        }

        public List<IUser> GetFollowers()
        {
            // TODO: Go to server and get all users following this entity by ID
            throw new System.NotImplementedException();
        }

        public int GetId()
        {
            return Id;
        }
    }
}
