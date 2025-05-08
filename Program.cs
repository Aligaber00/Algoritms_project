using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms_proj
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger a = new BigInteger(2003);
            BigInteger b = new BigInteger(3713);
            BigInteger c = new BigInteger(2563);
            BigInteger d = new BigInteger(7);
            RSA rSA = new RSA();
            BigInteger result = (rSA.Encrypt(a, d, b));
            Console.WriteLine(result);
            Console.WriteLine(rSA.Decrypt(result,c,b));

        }
    }
}
