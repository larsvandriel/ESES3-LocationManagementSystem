using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Entities
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
