using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZivAgam
{
    class CashRegister
    {

        public List<SubCashRegister> InformationTimeLine; // List of information about the cash register, orgenized by dates.

        // The building function of CashRegister class
        public CashRegister()
        {
            InformationTimeLine = new List<SubCashRegister>();
        }

        /* The function receives name of a costumer who is positive to covid 19.
         * The function prints the list of people who needs isolation beacause of that costumer.
         */
        public bool HasCorona(string name)
        {
            bool foundName = false;
            int i = 0;
            foreach (SubCashRegister subKupa in InformationTimeLine)
            {
                int x = 0;
                foreach (Customer costumer in InformationTimeLine[i].Costumers)
                {
                    if (name == InformationTimeLine[i].Costumers[x].Name)
                    {
                        foundName = true;
                        Console.WriteLine("All the above list need to enter isolation : \n");
                        InformationTimeLine[i].presentCostumers();
                        Console.WriteLine(InformationTimeLine[i].Employee.Name);
                    }
                    x++;
                }
                i++;
            }
            return foundName;
        }
    }
}