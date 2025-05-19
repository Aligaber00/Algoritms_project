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
            // test cases for the add,sub,mult
            //***********************************************************************************
            //string[] lines = File.ReadAllLines("MultiplyTestCases.txt");

            //int testCaseCount = int.Parse(lines[0].Trim());
            //List<string> numbers = new List<string>();

            //// Filter only number lines, ignoring blanks and the first line
            //for (int i = 1; i < lines.Length; i++)
            //{
            //    string trimmed = lines[i].Trim();
            //    if (!string.IsNullOrEmpty(trimmed))
            //    {
            //        numbers.Add(trimmed);
            //    }
            //}

            //// Process and output each sum directly
            //for (int i = 0; i < testCaseCount * 2; i += 2)
            //{
            //    BigInteger a = new BigInteger(numbers[i]);
            //    BigInteger b = new BigInteger(numbers[i + 1]);
            //    BigInteger sum = BigInteger.Multiply(a, b);
            //    Console.WriteLine(sum.ToString());
            //}
            //************************************************************************************




            // test cases for the complete test cases
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
            //***************************************************************************************************





            //string original = "All ASCII ~!@#^&*()_+=-` ook this is a very very long striiiiinngg very very long and wide";

            //BigInteger e = new BigInteger(17);
            //BigInteger d = new BigInteger(2753);
            //BigInteger n = new BigInteger(3233);        //must be bigger than 255
            //RSA rsa = new RSA();

            //var encrypted = rsa.EncryptString(original, e, n);
            //var decrypted = rsa.DecryptString(encrypted, d, n);

            //Console.WriteLine($"Original: {original}");
            //Console.WriteLine($"Decrypted: {decrypted}");
        }
    }
}
