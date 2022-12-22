using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZivAgam
{
     class CashRegisterManager
    {
        private const int _MIN_OPTION = 1;
        private const int _MAX_OPTION = 3;
        private const int _EXIT_OPTION = 3;
        private List<Employee> _listOfEmployees;
        private List<CashRegister> _listOfCashRegisters;
        private Menus _cashRegistersMenu;

        // The building function of CashRegisterManager class
        public CashRegisterManager(List<Employee> listOfEmployees, List<CashRegister> listOfCashRegisters)
        {
            _listOfEmployees = listOfEmployees;
            _listOfCashRegisters = listOfCashRegisters;
            _cashRegistersMenu = new Menus("Choose one of the three actions: \n" +
                    "1. Enter cash register number to see which employee operated her. \n" +
                    "2. Enter a costumer that is positive for covid 19. \n" +
                    "3. Exit to the main menu.");
        }

        /* The function check which cash register has the least costumers
         * and returns the number of the cash register.
         */
        public int CheckLowestCashRegister()
        {
            int i = 0;
            int x = 0;
            int lowestCostumers = _listOfCashRegisters[0].InformationTimeLine[x].CountCostumers;
            int lowestCostumersIndex = 0;
            foreach (CashRegister cashregister in _listOfCashRegisters)
            {
                int countOfCostumers = cashregister.InformationTimeLine[x].CountCostumers;
                if (countOfCostumers < lowestCostumers)
                {
                    lowestCostumers = countOfCostumers;
                    lowestCostumersIndex = i;
                }
                i++;
            }
            return lowestCostumersIndex;
        }

        /* The function receives costumer type object.
         * The function inserts the costumer to the cash register that has the least costumers.
         */
        public void InsertToLowestCashRegister(Customer costumer)
        {
            int index = CheckLowestCashRegister();
            int i = _listOfCashRegisters[index].InformationTimeLine.Count - 1;
            _listOfCashRegisters[index].InformationTimeLine[i].addCostumers(costumer);
        }

        // The function checks if the received employee number is valid
        private int _checkIfEmployeeNumberValid()
        {
            int cashRegisterNumber;
            string cashRegisterNumberString;
            bool parseSuccess;
            int countCashRegisters = _listOfCashRegisters.Count;
            do
            {
                Console.WriteLine("Enter a cash Register Number (one of " + countCashRegisters + " cash registers)");
                cashRegisterNumberString = Console.ReadLine();
                parseSuccess = int.TryParse(cashRegisterNumberString, out cashRegisterNumber);
            }
            while (!(parseSuccess && cashRegisterNumber > 0 && cashRegisterNumber <= countCashRegisters));
            return (cashRegisterNumber - 1);
        }

        /* The function prints the employee who operates the number 
         * of cash register that received
         */
        public void PrintEmployeeByCashRegister()
        {
            int cashRegisterNumber = _checkIfEmployeeNumberValid();
            string date = _listOfCashRegisters[cashRegisterNumber].InformationTimeLine[0].Date;
            string name = _listOfCashRegisters[cashRegisterNumber].InformationTimeLine[0].Employee.Name;
            Console.WriteLine("In date " + date + " the employee " + name + " operated cash register number " + (cashRegisterNumber + 1));
        }

        // The function checks if the received name is valid
        private string _getNameAndCheckIfValid()
        {
            string name;
            bool parseSuccess;
            int tryConverting;
            do
            {
                parseSuccess = false;
                Console.WriteLine("please insert his name.");
                name = Console.ReadLine();
                if (name == "")
                {
                    parseSuccess = true;
                }
                char[] check = new char[name.Length];
                check = name.ToCharArray();
                foreach (char i in check)
                {
                    if (int.TryParse(i.ToString(), out tryConverting))
                    {
                        parseSuccess = true;
                    }
                }
            }
            while (parseSuccess);
            return name;
        }

        // The function prints list of people that needs isolation.
        private void _printListOfIsolationNeeded()
        {
            string name = _getNameAndCheckIfValid();
            foreach (CashRegister cashRegister in _listOfCashRegisters)
            {
                if (cashRegister.HasCorona(name))
                {
                    return;

                }
            }
            Console.WriteLine("The name that was insert isn't appears in the supermarcet.");
        }

        // The function receives a choose and routes to the relevant function.
        public void RunChosenAction(int choose)
        {
            switch (choose)
            {
                case 1:
                    {
                        PrintEmployeeByCashRegister();
                        break;
                    }
                case 2:
                    {
                        _printListOfIsolationNeeded();
                        break;
                    }
                case 3:
                    {
                        break;
                    }
            }
        }

        // This is the main run function of the class. 
        public void Run()
        {
            int option;
            string userInput;
            bool parseSuccess;
            bool shouldExit = false;
            while (!shouldExit)
            {
                do
                {
                    Console.WriteLine(_cashRegistersMenu.SpecificMenu);
                    userInput = Console.ReadLine();
                    parseSuccess = int.TryParse(userInput, out option);
                }
                while (!(parseSuccess && option >= _MIN_OPTION && option <= _MAX_OPTION));
                if (option == _EXIT_OPTION)
                {
                    shouldExit = true;
                }
                else
                {
                    RunChosenAction(option);
                }
            }
        }
    }
}
