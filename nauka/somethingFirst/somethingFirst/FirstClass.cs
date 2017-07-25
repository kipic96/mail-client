using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace somethingFirst
{
    public class FirstClass
    {
        public FirstClass()
        {

        }
        public string str = string.Empty;

        public void WriteSomething(int param1, string param2)
        {
            Console.WriteLine("There are {0} shits and {1} up there", param1, param2);

        }
        public int autoShit { get; set; }
         
    }
}
