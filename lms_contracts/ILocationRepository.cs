﻿using LocationManagementSystem.Entities.Models;
using LocationManagementSystem.Entities.ShapedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Contracts
{
    public interface ILocationRepository: IRepositoryBase<Location>
    {
        ShapedEntity GetLocationById(Guid locationId, string fields);
        Location GetLocationById(Guid locationId);
        void CreateLocation(Location location);
        void UpdateLocation(Location dbLocation, Location location);
        void DeleteLocation(Location location);
    }
}
