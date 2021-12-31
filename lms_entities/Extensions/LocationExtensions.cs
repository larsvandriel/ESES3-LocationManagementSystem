using LocationManagementSystem.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Entities.Extensions
{
    public static class LocationExtensions
    {
        public static void Map(this Location dbLocation, Location location)
        {
            dbLocation.LocationNumber = location.LocationNumber;
            dbLocation.Type = location.Type;
            dbLocation.Name = location.Name;
            dbLocation.Size = location.Size;
            dbLocation.Address = location.Address;
            dbLocation.PhoneNumber = location.PhoneNumber;
            dbLocation.Email = location.Email;
            dbLocation.SiteManager = location.SiteManager;
            dbLocation.Departments = location.Departments;
            dbLocation.Rooms = location.Rooms;
            dbLocation.Inventory = location.Inventory;
            dbLocation.DateCreated = location.DateCreated;
            dbLocation.Deleted = location.Deleted;
            dbLocation.DateDeleted = location.DateDeleted;
        }
    }
}
