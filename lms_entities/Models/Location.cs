﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Entities.Models
{
    public class Location
    {
        public Guid Id { get; set; }
        public int LocationNumber { get; set; }
        public LocationType Type { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Employee SiteManager { get; set; }
        public List<Department> Departments { get; set; }
        public List<Room> Rooms { get; set; }
        public Inventory Inventory { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateDeleted { get; set; }
    }
}
