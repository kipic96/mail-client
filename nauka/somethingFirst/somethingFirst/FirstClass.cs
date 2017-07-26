using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace somethingFirst
{
    interface Ilog
    {
        void Log(string msgToLog);
    }
    public class UseClass : Ilog
    {
        
        public void meg()
        {
           int k = Console.Read();
           try
           {
               int n = 20 / (k - 48);
           }
           catch (DivideByZeroException ex)
           {
               Console.WriteLine(ex.Message);
           }
        }



        public void Log(string msgToLog)
        {
            Console.WriteLine("Hi {0}", "Marian");
        }


        public void method2()
        {
            int x = 20, y = 10;

            int result = x > y ? x + y : y;
        }


        public void method()
        {
            FirstClass first = new FirstClass();
            first.msg = 3; // works because have auto setter
            //first.msg2 = 3; // doesnt work, doesnt have a setter
            int bla = first.msg2; // have a getter
        }
    }
    public class FirstClass
    {
        private int msg3_; // field, they should be private
        public string str = string.Empty;// also field

        public int msg3 // property, it wraps the field
        {
            get { return msg3_; }
            set { msg3_ = value; }
        }

        public int msg2 { get; } // auto property, makes a field from it, only getter
        public int msg { get; set; } // auto property, makes a field from it
        public FirstClass() // constructor
        {

        }
        
        // method
        public void WriteSomething(int param1, string param2)
        {
            // writing with parameters to console
            Console.WriteLine("There are {0} shits and {1} up there", param1, param2);

        }
        public int autoShit { get; set; }
         
    }
}
