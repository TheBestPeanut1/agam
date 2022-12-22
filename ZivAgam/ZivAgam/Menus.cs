using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZivAgam
{
     class Menus
    {
        public string SpecificMenu { get; set; }

        // This is the building function of Menus class.
        public Menus(string manu)
        {
            SpecificMenu = manu;
        }
    }
}
