using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem.Entities.Parameters
{
    public class LocationParameters: QueryStringParameters
    {
        public LocationParameters()
        {
            OrderBy = "name";
        }

        public string Name { get; set; }
    }
}
