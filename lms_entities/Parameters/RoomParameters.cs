using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Entities.Parameters
{
    public class RoomParameters: QueryStringParameters
    {
        public RoomParameters()
        {
            OrderBy = "name";
        }

        public string Name { get; set; }
    }
}
