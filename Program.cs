using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Algoritms_proj
{
    public class Program
    {
        public static void Main()
        {
            string inputPath = "TestRSA.txt";
            string outputPath = "output_results2.txt";

            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Input file not found.");
                return;
            }

            string[] lines = File.ReadAllLines(inputPath);
            int testCount = int.Parse(lines[0]);
            List<string> outputResults = new List<string>();
            RSA rsa = new RSA();
            int index = 1;

            for (int i = 0; i < testCount; i++)
            {
                BigInteger N = new BigInteger(lines[index++]);
                BigInteger exponent = new BigInteger(lines[index++]);
                BigInteger message = new BigInteger(lines[index++]);
                int operation = int.Parse(lines[index++]);

                int start = Environment.TickCount;

                BigInteger result;
                if (operation == 0)
                    result = rsa.Encrypt(message, exponent, N); // encryption
                else
                    result = rsa.Decrypt(message, exponent, N); // decryption

                int end = Environment.TickCount;
                int elapsed = end - start;

                outputResults.Add(result.ToString());
                Console.WriteLine($"Test Case {i + 1}: {result}\nExecution Time: {elapsed} ms");
            }

            File.WriteAllLines(outputPath, outputResults);
            Console.WriteLine("Results saved to: " + outputPath);
        }
    }
}
