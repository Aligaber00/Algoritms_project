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
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "SampleRSA.txt";
            string outputPath = "output_results.txt";
            string[] lines = File.ReadAllLines(inputPath);

            int testCount = int.Parse(lines[0]);
            List<string> outputResults = new List<string>();
            RSA rsa = new RSA();
            int index = 1;

            for (int i = 0; i < testCount; i++)
            {
                BigInteger N = new BigInteger(int.Parse(lines[index++]));
                BigInteger exponent = new BigInteger(int.Parse(lines[index++]));
                BigInteger message = new BigInteger(int.Parse(lines[index++]));
                int operation = int.Parse(lines[index++]);

                Stopwatch sw = Stopwatch.StartNew();

                BigInteger result;
                if (operation == 0)
                    result = rsa.Encrypt(message, exponent, N);
                else
                    result = rsa.Decrypt(message, exponent, N);

                sw.Stop();
                double elapsedTimeMs = sw.Elapsed.TotalMilliseconds;

                outputResults.Add(result.ToString());
                Console.WriteLine($"Test Case {i + 1}: {result} (Time: {elapsedTimeMs:F3} ms)");
            }

            File.WriteAllLines(outputPath, outputResults);
            Console.WriteLine("Results saved to: " + outputPath);


        }
    }
}
