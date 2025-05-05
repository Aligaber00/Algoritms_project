using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms_proj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BigInteger a = new BigInteger(18477);
            BigInteger b = new BigInteger(1377);
            
            Console.WriteLine(BigInteger.Multiply(a, b));

        }
    }
}
