using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms_proj
{
    public class BigInteger
    {
        private List<int> digits;

        
        public BigInteger(int number)
        {
            digits = new List<int>();
            if (number == 0)
            {
                digits.Add(0);
                return;
            }

            while (number > 0)
            {
                digits.Add(number % 10);
                number /= 10;
            }
        }
        public BigInteger(string number)
        {
            digits = new List<int>();
            for (int i = number.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(number[i]))
                    digits.Add(number[i] - '0');
            }
            Trim();
        }



        public BigInteger(List<int> digits)
        {
            this.digits = digits;
            Trim();
        }

        private void Trim() // remove leading zeros               //O(N)
        {
            while (digits.Count > 1 && digits[digits.Count - 1] == 0) //O(N)
                digits.RemoveAt(digits.Count - 1);          //O(1)
        }

        public bool IsZero => digits.Count == 1 && digits[0] == 0; //0(1)
        public bool IsEven() => digits[0] % 2 == 0;                //0(1)

        public static bool IsSmaller(BigInteger a, BigInteger b)   //O(N)
        {

            bool aIsSmaller = a.digits.Count < b.digits.Count;      //O(1)
            if (a.digits.Count == b.digits.Count)                   //O(1)
            {
                for (int i = a.digits.Count - 1; i >= 0; i--)     //O(N)
                {
                    if (a.digits[i] != b.digits[i])                //==========
                    {
                        aIsSmaller = a.digits[i] < b.digits[i];
                        break;
                    }                                                 //O(1)
                }
            }
            return aIsSmaller;                                    //===========
        }
        public override string ToString()                //reverse the number again  O(N)
        {
            if (IsZero) return "0";

            var sb = new StringBuilder(digits.Count);
            // Reverse the digits to print most significant digit first
            for (int i = digits.Count - 1; i >= 0; i--)
            {
                sb.Append(digits[i]);
            }
            return sb.ToString();
        }

        // Addition 
        public static BigInteger Add(BigInteger a, BigInteger b)          //O(N)
        {
            List<int> result = new List<int>();                                //O(1)
            int carry = 0, maxLen = Math.Max(a.digits.Count, b.digits.Count);  //O(1)
            for (int i = 0; i < maxLen || carry > 0; i++)            //O(N)
            {                                                //==================
                int sum = carry;
                if (i < a.digits.Count) sum += a.digits[i];
                if (i < b.digits.Count) sum += b.digits[i];
                result.Add(sum % 10);                                //O(1)
                carry = sum / 10;
            }
            return new BigInteger(result);
        }                                                   //===================





        // subtraction 
        public static BigInteger Sub(BigInteger a, BigInteger b)         //O(N)
        {                                                              //==============
        
            if (IsSmaller(a , b))
            {
                Console.WriteLine("Cannot perform subtraction: first number is smaller than second.");
                return null; 
            }                                                                //O(1)
                                                                       
            int borrow = 0;
            List<int> resultDigits = new List<int>();
            int maxLen = Math.Max(a.digits.Count, b.digits.Count);
                                                                       //===============
            for (int i = 0; i < maxLen; i++)      //O(N)
            {                                                         //====================
                int diff = -borrow;
                if (i < a.digits.Count) diff += a.digits[i];
                if (i < b.digits.Count) diff -= b.digits[i];                  //O(1)
                borrow = diff < 0 ? 1 : 0;
                resultDigits.Add(diff + (borrow * 10));
            }                                                         //====================

            while (resultDigits.Count > 1 && resultDigits.Last() == 0)       //O(N)
                resultDigits.RemoveAt(resultDigits.Count - 1);               //O(1)

            return new BigInteger(resultDigits);                             //O(1)
        }




        // multiplcation
        public static BigInteger Multiply(BigInteger x, BigInteger y)          //O(N^1.585)
        {
            if (x.digits.Count == 1 && y.digits.Count == 1)                //====================
            {

                return new BigInteger(x.digits[0] * y.digits[0]);
            }
            int n = Math.Max(x.digits.Count, y.digits.Count);
            int half = (n + 1) / 2;                                                //O(1)

            BigInteger a = x.Firsthalf(half);
            BigInteger b = x.Second_half(half);
            BigInteger c = y.Firsthalf(half);
            BigInteger d = y.Second_half(half);
            // result = (ac) * 10^n + (ad + bc) * 10^half + bd             //======================
            BigInteger ac = Multiply(a, c);
            BigInteger bd = Multiply(b, d);
            // (ad + bc) = (a+c)(b+d) - ac - bd
            BigInteger a_plus_b = Add(a, b);   //O(1)
            BigInteger c_plus_d = Add(c, d);   //O(1)

            BigInteger first_value = Multiply(a_plus_b, c_plus_d);
            BigInteger second_value = Sub(first_value, ac);    //O(N)

            BigInteger ad_plus_bc = Sub(second_value, bd);     //O(N)

            BigInteger first_result = ac.Add_zeros(2 * half);  
            BigInteger second_result = ad_plus_bc.Add_zeros(half);
            BigInteger result = Add(first_result, second_result);  //O(N)
            return Add(result, bd);                                //O(N)

        }
        // Multiplcation helper functions
        public BigInteger Firsthalf(int num1)             //O(N)
        {
            if(num1 >=digits.Count) //O(1)
            {
                return new BigInteger(0);   //O(1)
            }
            List<int> first_half_digits = new List<int>(digits.GetRange(num1, digits.Count - num1));    //O(N)
            return new BigInteger(first_half_digits); //O(1)

        }                                                         
        public BigInteger Second_half(int num2)           //O(N)               
        {
            if (num2 >= digits.Count)   //O(1)
            {
                return new BigInteger(digits);      //O(1)
            }
            List<int> second_half_digits = new List<int>(digits.GetRange(0,num2));      //O(N)
            return new BigInteger(second_half_digits);  //O(1)
        }
        public BigInteger Add_zeros(int shift)             //O(N^2)
        {
            List<int> zero_added = new List<int>(digits);  //O(1)
            for (int i = 0; i < shift; i++)                //O(N)
            {
                zero_added.Insert(0, 0);                   //O(N)
            }
            return new BigInteger(zero_added);             //O(1)
        }


        // Divison 
        public static (BigInteger Quotient, BigInteger Remainder) Div(BigInteger a, BigInteger b)    //O(N^2)
        {
            if (b.IsZero)   //O(1)
                throw new DivideByZeroException("Cannot perform Division: division by zero is not allowed.");   //O(1)

            if (IsSmaller(a, b))         //O(N)
                return (new BigInteger(0), a);   //O(1)

            BigInteger twoB = Add(b, b);         //O(N)
            var (q, r) = Div(a, twoB);           

            BigInteger twoQ = Add(q, q);         //O(N)

            if (IsSmaller(r, b))                 //O(N)
                return (twoQ, r);                //O(1)
            else
                return (Add(twoQ, new BigInteger(1)), Sub(r, b));    //O(N) 
        }
        // Mod func for the Mod_pow
        public static BigInteger Mod(BigInteger a, BigInteger b)   //O(N^2)
        {
            var (_, remainder) = Div(a, b);         //O(N^2)
            return remainder;                       //O(1)
        }

        // Mod_pow used for encrypt and decrypt
        public static BigInteger Mod_pow(BigInteger basenum, BigInteger exponent, BigInteger modulus)     //O(n^2log exponent)
        {                                                                    //===========
            if (modulus.IsZero)
                throw new DivideByZeroException("modulus cannot be zero");
            if (exponent.IsZero)                                                 //O(1)
                return new BigInteger(1);
            if (exponent.digits.Count == 1 && exponent.digits[0] == 1)      //============
                return Mod(basenum, modulus);     //O(N^2)
            BigInteger mid = Mod_pow(basenum, exponent.Divide_by2(), modulus);
            BigInteger result = Mod(Multiply(mid, mid), modulus);        //O(N^2)

            if (!exponent.IsEven())             //O(1)
            {
                result = Mod(Multiply(result, basenum), modulus);         //O(N^2)
            }
            return result;        //O(1)

        }

        //helper for Mod_pow
        public BigInteger Divide_by2(int shift = 1)     //O(N^2)
        {
            if (shift <= 0) return new BigInteger(this.digits.ToList());    //============
            if (IsZero) return new BigInteger(0);  
                                                                                //O(1)
            List<int> result = new List<int>();     
            int carry = 0;     
                                                                            //============

            for (int i = digits.Count - 1; i >= 0; i--)   //O(N^2)
            {
                int current = digits[i] + carry * 10;     //O(1)
                result.Insert(0, current / 2);              //O(N^2)
                carry = current % 2;                      //O(1)
            }


            //while (result.Count > 1 && result.Last() == 0)   //O(N)
            //    result.RemoveAt(result.Count - 1);     //O(1)

            return new BigInteger(result);      //O(1)
        }                                          
        // bonus 1 -> string
        public static BigInteger CharToInt(char c)     //O(1)
        {
            return new BigInteger((int)c);    //O(1)
        }

        public static char IntToChar(BigInteger num)    //O(1)      //normally O(L) L = length, but max num is 255 so L = 3
        {
            int ASCIIval = 0;  //O(1)
            int pow = 1;     //O(1)

            for (int i = 0; i < num.digits.Count; i++) //O(1)
            {
                ASCIIval += num.digits[i] * pow;      //O(1)
                pow *= 10;       //O(1)
            }

            return (char)ASCIIval;      //O(1)
        }

    }
    
    
}