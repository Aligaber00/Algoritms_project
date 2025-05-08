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
        public BigInteger Encrypt(BigInteger message,BigInteger e ,BigInteger n)
        {
            return BigInteger.Mod_pow(message, e, n);
        }

        public BigInteger Decrypt(BigInteger ciphertext,BigInteger d, BigInteger n)
        {
            return BigInteger.Mod_pow(ciphertext, d, n);
        }
    }

}
