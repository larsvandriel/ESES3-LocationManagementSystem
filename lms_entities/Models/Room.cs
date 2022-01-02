using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Entities.Models
{
    public class Room: IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
