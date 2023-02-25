using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yard_Management_System.Entity;

namespace Database.Entity
{
    public class Storage
    {
        public Guid Id { get; set; }
        public List<Trip> Trips { get; set; }
    }
}
