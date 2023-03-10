using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Numerics;

namespace RSA_Algorythm
{
    internal class RSA
    {
        public int p, q, n, phi, e, d;

        public RSA()
        {
            p = GetRandomPrimeNumber();
            q = GetRandomPrimeNumber();
            n = p * q;
            phi = (p - 1) * (q - 1);
            e = GetEncryptionExponent(phi);
            d = GetDecryptionExponent(e, phi);
        }

        static int GetRandomPrimeNumber()
        {
            // Генератор простых чисел
            Random rand = new Random();
            int num = rand.Next(2, 100);
            while (!IsPrime(num))
            {
                num = rand.Next(2, 100);
            }
            return num;
        }

        static bool IsPrime(int num)
        {
            if (num < 2)
            {
                return false;
            }
            for (int i = 2; i <= Math.Sqrt(num); i++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static int GetEncryptionExponent(int phi)
        {
            Random rand = new Random();
            int e = rand.Next(2, phi);
            while (GCD(e, phi) != 1)
            {
                e = rand.Next(2, phi);
            }
            return e;
        }
        static int GCD(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }
            return GCD(b, a % b);
        }
        static int GetDecryptionExponent(int e, int phi)
        {
            int d = 1;
            while (true)
            {
                if (((d * e) % phi) == 1)
                {
                    break;
                }
                d++;
            }
            return d;
        }
        public int[] EncryptMessage(string message, int e, int n)
        {
            int[] encryptedMessage = new int[message.Length];
            for (int i = 0; i < message.Length; i++)
            {
                // Encrypt each character in the message using the public key (e, n)
                encryptedMessage[i] = ModPow((int)message[i], e, n);
            }
            return encryptedMessage;
        }
        public string DecryptMessage(int[] encryptedMessage, int d, int n)
        {
            StringBuilder decryptedMessage = new StringBuilder();
            foreach (int c in encryptedMessage)
            {
                // Decrypt each character in the encrypted message using the private key (d, n)
                decryptedMessage.Append((char)ModPow(c, d, n));
            }
            return decryptedMessage.ToString();
        }
        static int ModPow(int baseNum, int exponent, int modulus)
        {
            // This is a simple way to calculate baseNum^exponent mod modulus
            int result = 1;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                {
                    result = (result * baseNum) % modulus;
                }
                baseNum = (baseNum * baseNum) % modulus;
                exponent >>= 1;
            }
            return result;
        }
    }
}
