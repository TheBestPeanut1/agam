using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZivAgam
{
     class LineManager
    {

        private const int _MIN_OPTION = 1;
        private const int _MAX_OPTION = 4;
        private const int _EXIT_OPTION = 4;
        private Queue<Customer> _Line; // The line of customers outside the supermarcet.
        private CashRegisterManager _cashRegisterManager;
        private Menus _lineManagerManu;
        private List<CashRegister> _listOfCashRegisters;

        // This function is the building function of LineManager class.
        public LineManager(CashRegisterManager cashRegisterManagerValue, List<CashRegister> listOfCashRegisters)
        {
            _cashRegisterManager = cashRegisterManagerValue;
            _listOfCashRegisters = listOfCashRegisters;
            _Line = new Queue<Customer>();
            _lineManagerManu = new Menus("Choose one of the four actions: \n" +
                    "1. Add costumer to the line. \n" +
                    "2. Enter costumers to the supermarcet. \n" +
                    "3. Print the line information. \n" +
                    "4. Exit to the main menu.");

        }

        // הפונקציה מכניסה לקוח לתור אם הוא עומד בקריטריונים
        public void AddCostumer(Queue<Customer> line)
        {
            Customer costumer = new Customer();
            if (costumer.DecideIfMeetTheConditions())
            {
                line.Enqueue(costumer);
                Console.WriteLine("The line has now : " + line.Count + " costumers");
            }
        }

        // הפונקציה מציגה את הכמות לקוחות בחנות.
        public void InsertToSuper(Queue<Customer> line, int numberOfPeopleInsert)
        {
            for (int i = 0; i < numberOfPeopleInsert; i++)
            {
                _cashRegisterManager.InsertToLowestCashRegister(line.Dequeue());
            }
            Console.WriteLine("Now in line there is " + line.Count + " costumers.");
        }

        // הפונקציה מדפיסה את הכמות לקוחות בתור לפי סדר
        public void PrintLine(Queue<Customer> line)
        {
            Console.WriteLine("Here is the costumers in line, by order: \n");
            foreach (Customer customer in line)
            {
                Console.WriteLine(customer.Name);
            }
        }

        // The function receives a choose and routes to the relevant function.
        private int _checkIfNumberOfCostumersValid()
        {
            bool parseSuccess;
            string numberCostumersString;
            int numberCostumers;
            Console.WriteLine("Note to yourself that there is " + _Line.Count + " costumers in line.");
            do
            {
                Console.WriteLine("How many costumers do you want to enter the supermarket?");
                numberCostumersString = Console.ReadLine();
                parseSuccess = int.TryParse(numberCostumersString, out numberCostumers);
            }
            while (!(parseSuccess && numberCostumers >= 0 && numberCostumers <= _Line.Count));
            return numberCostumers;
        }

        // The function receives a choose and routes to the relevant function.
        public void RunChosenAction(int choose)
        {
            switch (choose)
            {
                case 1:
                    {
                        AddCostumer(_Line);
                        break;
                    }
                case 2:
                    {
                        if (_Line.Count == 0)
                        {
                            Console.WriteLine("There isn't costumers in Line.");
                            break;
                        }
                        else
                        {
                            InsertToSuper(_Line, _checkIfNumberOfCostumersValid());
                            break;
                        }
                    }
                case 3:
                    {
                        PrintLine(_Line);
                        break;
                    }
                case 4:
                    {
                        break;
                    }
            }
        }

        // the main run function of the class. 
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
                    Console.WriteLine(_lineManagerManu.SpecificMenu);
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
