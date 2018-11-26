using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanSupply.Pocos
{
    public class Supplier
    {
        public String Name { get; set; }
        public Point Location { get; set; }
        public Supplier()
        {

        }
        public Supplier(string n, Point p)
        {
            Name = n;
            Location = p;
        }
    }
}
