using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZivAgam
{
    class Customer : Person  // מחלקת לקוח
    {

        public bool HaveMask { get; set; }
        public bool NeedIsolation { get; set; }
        private bool _parseSuccess;

        // הפונקציה בודקת אם השם המתקבל תקין
        private void _getNameAndCheckIfValid()
        {
            int tryConverting;
            do
            {
                _parseSuccess = false;
                Console.WriteLine("Enter your name");
                Name = Console.ReadLine();
                if (Name == "")
                {
                    _parseSuccess = true;
                }
                char[] check = new char[Name.Length];
                check = Name.ToCharArray();
                foreach (char i in check)
                {
                    if (int.TryParse(i.ToString(), out tryConverting))
                    {
                        _parseSuccess = true;
                    }
                }
            }
            while (_parseSuccess);
        }

        // הפונקציה בודקת אם הטמפרטורה המתקבלת תקינה
        private void _getBodyHeatAndCheckIfValid()
        {
            double bodyHeatTry;
            string tryParse;
            do
            {
                Console.WriteLine("Enter body heat");
                tryParse = Console.ReadLine();
                _parseSuccess = double.TryParse(tryParse, out bodyHeatTry);
            }
            while (!(_parseSuccess && bodyHeatTry > 35 && bodyHeatTry < 42));
            BodyHeat = bodyHeatTry;
        }

        // הפונקציה בודקת אם התשובה המתקבלת של המסכה תקינה
        private string _getHaveMaskAndCheckIfValid()
        {
            string answer;
            do
            {
                Console.WriteLine("Do you have a mask? (Answer yes or no)");
                answer = Console.ReadLine();
            }
            while (!(answer == "yes" || answer == "no"));
            return answer;
        }

        // הפונקציה בודקת אם התשובה המתקבלת על בידוד תקינה
        private string _needisolationMaskAndCheckIfValid()
        {
            string answer;
            do
            {
                Console.WriteLine("Do you need to be in isolation? (Answer yes or no)");
                answer = Console.ReadLine();
            }
            while (!(answer == "yes" || answer == "no"));
            return answer;
        }

        // הפונקציה בודקת אם הלקוח עומד בדרישות להיכנס לחנות
        public bool DecideIfMeetTheConditions()
        {
            _getNameAndCheckIfValid();
            _getBodyHeatAndCheckIfValid();
            if (BodyHeat > 38)
            {
                Console.WriteLine("Your Body heat is over 38! You can't enter the supermarcet");
                return false;
            }
            if (_getHaveMaskAndCheckIfValid() == "yes")
            {
                HaveMask = true;
            }
            else
            {
                Console.WriteLine("You don't have mask! You can't enter the supermarcet");
                return false;
            }
            if (_needisolationMaskAndCheckIfValid() == "no")
            {
                NeedIsolation = false;
            }
            else
            {
                Console.WriteLine("You need to be in isolation! You can't enter the supermarcet");
                return false;
            }
            return true;
        }

    }
}
