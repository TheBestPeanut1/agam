using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZivAgam
{
    class Employee : Customer // מחלקת עובד
    {
        public TimeSpan StartTime;
        public TimeSpan FinishTime;
        
        public Employee()
        {
            Name = null;
            StartTime = new TimeSpan();
            FinishTime = new TimeSpan();
        }
        public override string ToString()
        {
            return "Information about the employer " + Name + ": " +
                "Start time: " + StartTime.ToString() + ". " +
                "Finish time: " + FinishTime.ToString() + ".";
        }

        
        
    }
}

