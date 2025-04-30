using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms_proj
{
   
       public class BigInteger
        {
            private List<byte> digits;
            public BigInteger(string number)
            {
                digits = new List<byte>();
                for (int i = number.Length - 1; i >= 0; i--)
                    digits.Add((byte)(number[i] - '0'));
                Trim();

            }
            public BigInteger(List<byte> digits)
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

            public override string ToString() // return it to string from the digit list
            {
                StringBuilder sb = new StringBuilder();
                for (int i = digits.Count - 1; i >= 0; i--)
                    sb.Append(digits[i]);
                return sb.ToString();
            }


            public static BigInteger Add(BigInteger a, BigInteger b)
            {
                List<byte> result = new List<byte>();
                int carry = 0, maxLen = Math.Max(a.digits.Count, b.digits.Count);
                for (int i = 0; i < maxLen || carry != 0; i++)
                {
                    int sum = carry;
                    if (i < a.digits.Count) sum += a.digits[i];
                    if (i < b.digits.Count) sum += b.digits[i];
                    result.Add((byte)(sum % 10));
                    carry = sum / 10;
                }
                return new BigInteger(result);
            }
        }
    
}
