using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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

        

        public BigInteger(List<int> digits)
        {
            this.digits = digits;
            Trim();
        }

        private void Trim() // remove leading zeros
        {
            while (digits.Count > 1 && digits[digits.Count - 1] == 0)
                digits.RemoveAt(digits.Count - 1);
        }

        public bool IsZero => digits.Count == 1 && digits[0] == 0;
        public bool IsEven() => digits[0] % 2 == 0;

        private bool IsSmaller(BigInteger a , BigInteger b)
        {
           
            bool aIsSmaller = a.digits.Count < b.digits.Count;
            if (a.digits.Count == b.digits.Count)
            {
                for (int i = a.digits.Count - 1; i >= 0; i--)
                {
                    if (a.digits[i] != b.digits[i])
                    {
                        aIsSmaller = a.digits[i] < b.digits[i];
                        break;
                    }
                }
            }
            return aIsSmaller;
        }
        public override string ToString()
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

        public static BigInteger Add(BigInteger a, BigInteger b)
        {
            List<int> result = new List<int>();
            int carry = 0, maxLen = Math.Max(a.digits.Count, b.digits.Count);
            for (int i = 0; i < maxLen || carry > 0; i++)
            {
                int sum = carry;
                if (i < a.digits.Count) sum += a.digits[i];
                if (i < b.digits.Count) sum += b.digits[i];
                result.Add(sum % 10);
                carry = sum / 10;
            }
            return new BigInteger(result);
        }





        public static BigInteger Sub(BigInteger a, BigInteger b)
        {
        
            if (a.IsSmaller(a , b))
            {
                Console.WriteLine("Cannot perform subtraction: first number is smaller than second.");
                return null; 
            }

            int borrow = 0;
            List<int> resultDigits = new List<int>();
            int maxLen = Math.Max(a.digits.Count, b.digits.Count);
           
            for (int i = 0; i < maxLen; i++)
            {
                int diff = -borrow;
                if (i < a.digits.Count) diff += a.digits[i];
                if (i < b.digits.Count) diff -= b.digits[i];
                borrow = diff < 0 ? 1 : 0;
                resultDigits.Add(diff + (borrow * 10));
            }

            while (resultDigits.Count > 1 && resultDigits.Last() == 0)
                resultDigits.RemoveAt(resultDigits.Count - 1);

            return new BigInteger(resultDigits);
        }





        public static BigInteger Multiply(BigInteger x, BigInteger y)
        {
            if (x.digits.Count == 1 && y.digits.Count == 1)
            {
                
                return new BigInteger(x.digits[0] * y.digits[0]);
            }
            int n = Math.Max(x.digits.Count,y.digits.Count);
            int half = (n + 1) / 2;

            BigInteger a = x.Firsthalf(half);
            BigInteger b = x.Second_half(half);
            BigInteger c = y.Firsthalf(half);
            BigInteger d = y.Second_half(half);

            BigInteger ac = Multiply(a, c);
            BigInteger bd = Multiply(b, d);
            BigInteger a_plus_b = Add(a, b);
            BigInteger c_plus_d = Add(c, d);
            
            BigInteger first_value = Multiply(a_plus_b, c_plus_d);
            BigInteger second_value = Sub(first_value, ac);

            BigInteger ad_plus_bc = Sub(second_value, bd);

            BigInteger first_result = ac.Add_zeros(2 * half);
            BigInteger second_result = ad_plus_bc.Add_zeros(half);
            BigInteger result = Add(first_result, second_result);
            return Add(result, bd);

        }
        public BigInteger Firsthalf(int num1)
        {
            if(num1 >=digits.Count)
            {
                return new BigInteger(0);
            }
            List<int> first_half_digits = new List<int>(digits.GetRange(num1,digits.Count - num1));
            return new BigInteger(first_half_digits);

        }
        public BigInteger Second_half(int num2)
        {
            if (num2 >= digits.Count)
            {
                return new BigInteger(digits);
            }
            List<int> second_half_digits = new List<int>(digits.GetRange(0,num2));
            return new BigInteger(second_half_digits);
        }
        public BigInteger Add_zeros(int shift)
        {
            List<int> zero_added = new List<int>(digits);
            for (int i = 0; i < shift; i++)
            {
                zero_added.Insert(0, 0);
            }
            return new BigInteger(zero_added);
        }

        public static (BigInteger Quotient, BigInteger Remainder) Div(BigInteger a, BigInteger b)
        {
            if (b.IsZero)
                throw new DivideByZeroException("Cannot perform Division: division by zero is not allowed.");

            if (a.IsSmaller(a, b))
                return (new BigInteger(0), a);

            BigInteger twoB = Add(b, b);
            var (q, r) = Div(a, twoB);

            BigInteger twoQ = Add(q, q);

            if (a.IsSmaller(r, b))
                return (twoQ, r);
            else
                return (Add(twoQ, new BigInteger(1)), Sub(r, b));
        }

    }
    
    
}

    
