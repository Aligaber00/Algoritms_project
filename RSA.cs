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
        public BigInteger Encrypt(BigInteger message, BigInteger e, BigInteger n)               //O(n^2log e)
        {
            return BigInteger.Mod_pow(message, e, n);                       //O(n^2log e)
        }

        public List<BigInteger> EncryptString(string message, BigInteger e, BigInteger n)   //O(L * n^2log e)
        {
            List<BigInteger> EncryptedChars = new List<BigInteger>();       //O(1)

            foreach (char c in message)         //O(L * n^2log e)        L is length of string
            {
                BigInteger ASCIIval = BigInteger.CharToInt(c);  //O(1)
                BigInteger EncryptedChar = Encrypt(ASCIIval, e, n); //O(n^2log e)
                EncryptedChars.Add(EncryptedChar);  //O(1)
            }

            return EncryptedChars;  //O(1)
        }

        public BigInteger Decrypt(BigInteger ciphertext, BigInteger d, BigInteger n)                //O(n^2log d)
        {
            return BigInteger.Mod_pow(ciphertext, d, n);                    //O(n^2log d)
        }

        public string DecryptString(List<BigInteger> EncryptedChars, BigInteger d, BigInteger n)     //O(C * n^2log d)
        {
            StringBuilder message = new StringBuilder();    //O(1)

            foreach (BigInteger Char in EncryptedChars)     //O(C * n^2log d)        C is number of Chars
            {
                BigInteger DecryptedChar = Decrypt(Char, d, n);     //O(n^2log d)
                char Letter = BigInteger.IntToChar(DecryptedChar);      //O(1)
                message.Append(Letter);     //O(1)
            }
            return message.ToString();      //O(1)
        }

    }

}