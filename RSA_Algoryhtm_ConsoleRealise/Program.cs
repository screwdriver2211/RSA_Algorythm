
using System.Numerics;
using System.Text;

namespace RSA_Algoryhtm_ConsoleRealise
{
    internal class Program
    {
        static void Main()
        {
            // Generate two random prime numbers
            int p = GetRandomPrimeNumber();
            int q = GetRandomPrimeNumber();

            // Calculate n and phi(n)
            int n = p * q;
            int phi = (p - 1) * (q - 1);

            // Choose an encryption exponent e
            int e = GetEncryptionExponent(phi);

            // Calculate the decryption exponent d
            int d = GetDecryptionExponent(e, phi);

            // Print the public key (e, n) and private key (d, n)
            Console.WriteLine("Public key: ({0}, {1})", e, n);
            Console.WriteLine("Private key: ({0}, {1})", d, n);

            // Encrypt a message using the public key
            string message = "Ariskin";
            int[] encryptedMessage = EncryptMessage(message, e, n);

            // Decrypt the encrypted message using the private key
            string decryptedMessage = DecryptMessage(encryptedMessage, d, n);

            // Print the original and decrypted messages
            Console.WriteLine("Original message: {0}", message);
            Console.WriteLine("Decrypted message: {0}", decryptedMessage);
        }

        static int GetRandomPrimeNumber()
        {
            // This is a simple way to generate random prime numbers
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
            // Choose an encryption exponent e such that 1 < e < phi and e is coprime to phi
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
            // Calculate the decryption exponent d such that (d * e) mod phi = 1
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

        static int[] EncryptMessage(string message, int e, int n)
        {
            int[] encryptedMessage = new int[message.Length];
            for (int i = 0; i < message.Length; i++)
            {
                // Encrypt each character in the message using the public key (e, n)
                encryptedMessage[i] = ModPow((int)message[i], e, n);
            }
            return encryptedMessage;
        }
        static string DecryptMessage(int[] encryptedMessage, int d, int n)
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
