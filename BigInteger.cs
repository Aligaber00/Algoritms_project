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

        public static BigInteger Sub(BigInteger a, BigInteger b)
        {
            // check is answer is negative
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

            int borrow = 0;
            List<byte> resultDigits = new List<byte>();

            if (aIsSmaller)
            {
                for (int i = 0; i < b.digits.Count; i++)
                {
                    int diff = b.digits[i] - (i < a.digits.Count ? a.digits[i] : 0) - borrow;
                    borrow = diff < 0 ? 1 : 0;
                    resultDigits.Add((byte)(diff + (borrow * 10)));
                }
                return new BigInteger("-" + new BigInteger(resultDigits).ToString());
            }

            // sub logic
            int maxLen = Math.Max(a.digits.Count, b.digits.Count);
            for (int i = 0; i < maxLen; i++)
            {
                int diff = -borrow;
                if (i < a.digits.Count) diff += a.digits[i];
                if (i < b.digits.Count) diff -= b.digits[i];
                borrow = diff < 0 ? 1 : 0;
                resultDigits.Add((byte)(diff + (borrow * 10)));
            }

            while (resultDigits.Count > 1 && resultDigits.Last() == 0)
                resultDigits.RemoveAt(resultDigits.Count - 1);

            return new BigInteger(resultDigits);
        }

    }

}
