using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZivAgam
{
     class SubCashRegister
    {

        public List<Customer> Costumers;
        public List<Product> Products;
        public Employee Employee;
        public string Date { get; set; }
        public int CountCostumers;

        // This function is the building function of SubCashRegister class.
        public SubCashRegister()
        {
            Costumers = new List<Customer>();
            Products = new List<Product>();
            CountCostumers = 0;
        }

        //This function inserts the received costumer to the costumers list of the specific cash register.
        public void addCostumers(Customer costumer)
        {
            Costumers.Add(costumer);
            CountCostumers++;
        }

        //This function inserts the received product to the products list of the specific cash register.
        public void addProduct(Product product)
        {
            Products.Add(product);
        }

        //This function presents the costumers in the cash register.
        public void presentCostumers()
        {
            foreach (Customer costumer in Costumers)
            {
                Console.WriteLine(costumer.Name);
            }

        }

        //This function presents the products in the cash register.
        public void presentProducts()
        {
            Console.WriteLine("Here is the list of products purchased in the cash register: \n");
            foreach (Product product in Products)
            {
                Console.WriteLine(product.Name);
            }
            
        }
    }
}
