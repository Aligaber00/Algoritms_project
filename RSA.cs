using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Algoritms_proj
{
    public class RSA
    {
        public BigInteger Encrypt(BigInteger message, BigInteger e, BigInteger n)
        {
            return BigInteger.Mod_pow(message, e, n);
        }

        public List<BigInteger> EncryptString(string message, BigInteger e, BigInteger n)
        {
            List<BigInteger> EncryptedChars = new List<BigInteger>();

            foreach (char c in message)
            {
                BigInteger ASCIIval = BigInteger.CharToInt(c);
                BigInteger EncryptedChar = Encrypt(ASCIIval, e, n);
                EncryptedChars.Add(EncryptedChar);
            }

            return EncryptedChars;
        }

        public BigInteger Decrypt(BigInteger ciphertext, BigInteger d, BigInteger n)
        {
            return BigInteger.Mod_pow(ciphertext, d, n);
        }

        public string DecryptString(List<BigInteger> EncryptedChars, BigInteger d, BigInteger n)
        {
            StringBuilder message = new StringBuilder();

            foreach (BigInteger Char in EncryptedChars)
            {
                BigInteger DecryptedChar = Decrypt(Char, d, n);
                char Letter = BigInteger.IntToChar(DecryptedChar);
                message.Append(Letter);
            }
            return message.ToString();
        }

    }

}
