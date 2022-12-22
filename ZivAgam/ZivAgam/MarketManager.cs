using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZivAgam
{
    class MarketManager
    {

        private const int _MIN_OPTION = 1;
        private const int _MAX_OPTION = 3;
        private const int _EXIT_OPTION = 3;
        private int _numberOfEmployeesAndCashRegisters = 3;
        private LineManager _LineManager;
        private CashRegisterManager _CashRegisterManager;
        private List<Employee> _listOfemployees;
        private List<CashRegister> _listOfCashRegisters;
        private Menus _mainMenu;

        // This is the building function of MarketManager class.
        public MarketManager()
        {
            _listOfemployees = new List<Employee>(_numberOfEmployeesAndCashRegisters);
            _listOfCashRegisters = new List<CashRegister>(_numberOfEmployeesAndCashRegisters);
            _CashRegisterManager = new CashRegisterManager(_listOfemployees, _listOfCashRegisters);
            _LineManager = new LineManager(_CashRegisterManager, _listOfCashRegisters);
            _mainMenu = new Menus("Choose one of the three action: \n" +
                    "1. Enter the line manager. \n" +
                    "2. Enter the cash register manager. \n" +
                    "3. Close the system. \n");
        }

        // הפונקציה בונה רשימה של כל העובדים הפועלים
        private void insertEmployees()
        {
            int currentNumberOfEmployeesAndCashRegisters = _numberOfEmployeesAndCashRegisters;
            Console.WriteLine("Insert the employees information: ");
            string startTimeString;
            string hour;
            string minutes;
            int validHour;
            int validMinutes;
            bool parseSuccess1;
            bool parseSuccess2;
            for (int i = 0; i < _numberOfEmployeesAndCashRegisters; i++)
            {
                Employee employee = new Employee();
                if (employee.DecideIfMeetTheConditions())
                {
                    do
                    {
                        do
                        {
                            Console.WriteLine("insert the start time of " + employee.Name + ". (keep on the following format: HH:MM)");
                            startTimeString = Console.ReadLine();
                        }
                        while (!(startTimeString.Contains(":")));
                        hour = startTimeString.Split(':')[0];
                        minutes = startTimeString.Split(':')[1];
                        parseSuccess1 = int.TryParse(hour, out validHour);
                        parseSuccess2 = int.TryParse(minutes, out validMinutes);
                    }
                    while (!(parseSuccess1 && parseSuccess2 && validHour >= 0 && validHour < 24 && validMinutes >= 0 && validMinutes < 60));
                    TimeSpan startTimeObject = new TimeSpan(validHour, validMinutes, 00);
                    employee.StartTime = startTimeObject;
                    _listOfemployees.Add(employee);
                }
                else
                {
                    Console.WriteLine("The employer " + employee.Name + " can't enter the work today," +
                        " and will receive a fine of 40 shekels. \n" +
                        "He needs to pay this fine by working extra one hour and twenty minutes in the next shift.");
                    currentNumberOfEmployeesAndCashRegisters--;
                }
            }
            _numberOfEmployeesAndCashRegisters = currentNumberOfEmployeesAndCashRegisters;
        }

        // הפונקציה מחליטה את שעת סיום העבודה של העובד
        private void setEmployeeFinishTime()
        {
            string finishTimeString;
            string hour;
            string minutes;
            int validHour;
            int validMinutes;
            bool parseSuccess1;
            bool parseSuccess2;
            foreach (Employee employee in _listOfemployees)
            {
                do
                {
                    do
                    {
                        Console.WriteLine("insert the finisht time of " + employee.Name + ". (keep on the following format: HH:MM)");
                        finishTimeString = Console.ReadLine();
                    }
                    while (!(finishTimeString.Contains(":")));
                    hour = finishTimeString.Split(':')[0];
                    minutes = finishTimeString.Split(':')[1];
                    parseSuccess1 = int.TryParse(hour, out validHour);
                    parseSuccess2 = int.TryParse(minutes, out validMinutes);
                }
                while (!(parseSuccess1 && parseSuccess2 && validHour >= 0 && validHour < 24 && validMinutes >= 0 && validMinutes < 60));
                TimeSpan finishTimeObject = new TimeSpan(validHour, validMinutes, 00);
                employee.FinishTime = finishTimeObject;
            }
        }

        // הפונקציה בונה רשימה של כל הקופות הפעילות
        private void buildCashRegistersList()
        {
            string date;
            for (int i = 0; i < _numberOfEmployeesAndCashRegisters; i++)
            {
                CashRegister cashRegister = new CashRegister();
                SubCashRegister subCashRegister = new SubCashRegister();
                DateTime localDate = DateTime.Now;
                date = localDate.ToString("yyyy-MM-dd");
                subCashRegister.Date = date;
                subCashRegister.Employee = _listOfemployees[i];
                cashRegister.InformationTimeLine.Add(subCashRegister);
                _listOfCashRegisters.Add(cashRegister);
            }
        }

        // This is the main run function of the class. 
        public void run()
        {
            int option;
            string userInput;
            bool parseSuccess;
            bool shouldExit = false;
            insertEmployees();
            if (_numberOfEmployeesAndCashRegisters == 0)
            {
                Console.WriteLine("There isn't any employees available. The system will exit.");
                return;
            }
            buildCashRegistersList();
            while (!shouldExit)
            {
                do
                {
                    Console.WriteLine(_mainMenu.SpecificMenu);
                    userInput = Console.ReadLine();
                    parseSuccess = int.TryParse(userInput, out option);
                }
                while (!(parseSuccess && option >= _MIN_OPTION && option <= _MAX_OPTION));
                if (option == _EXIT_OPTION)
                {
                    setEmployeeFinishTime(); /*Important to mention, the input of the finish time assumes that the 
                                              * information stored in a data base, so you could get it always. 
                                              * I assumed that when the manager wants to exit the system,
                                              * you could insert finish time
                                              */
                    foreach (Employee employee in _listOfemployees)
                    {
                        Console.WriteLine(employee.ToString());
                    }
                    shouldExit = true;
                }
                else
                {
                    runChosenAbility(option);
                }
            }

        }

        // The function receives a choose and routes to the relevant function.
        public void runChosenAbility(int choose)
        {
            switch (choose)
            {
                case 1:
                    {
                        _LineManager.Run();
                        break;
                    }
                case 2:
                    {
                        _CashRegisterManager.Run();
                        break;
                    }
            }
        }

    }
    }

