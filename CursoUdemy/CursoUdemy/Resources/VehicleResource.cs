using CursoUdemy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CursoUdemy.Resources
{
    public class VehicleResource
    {

        public int Id { get; set; }

        public int ModelId { get; set; }

        public bool IsRegistered { get; set; }

        public ContactResource Contact { get; set; }

        public ICollection<int> VehicleFeature { get; set; }

        public VehicleResource()
        {
            VehicleFeature = new Collection<int>();
        }

    }
}
