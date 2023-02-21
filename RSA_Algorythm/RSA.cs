using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace RSA_Algorythm
{
    internal class RSA
    {
        private int p, q, n, phi_n, e, d;
        private List<int> primeNumbersList = new List<int>();


        public RSA()
        {
            Random random = new Random();
            primeNumbersList = checkPrime(10000);
            p = random.Next(primeNumbersList.Count);
            q = random.Next(primeNumbersList.Count);
            n = p * q;
            phi_n = (p - 1) * (q - 1);
            e = Generate_E();
            d = Generate_D();
        }

        private List<int> checkPrime(int number)
        {
            List<int> primeNumbersList = new List<int>();
            if (number < 2)
            {
                MessageBox.Show("Нет простых чисел в диапазоне");
            }
            else
            {
                for(int i = 2; i < number; i++)
                {
                    if(number % i != 0)
                    {
                        primeNumbersList.Add(i);
                    }
                }
            }
            return primeNumbersList;
            
        }

        private int Generate_E()
        {
            Random random = new Random();
            while (true)
            {
                e = random.Next(2, phi_n);
                if(GCD(e, phi_n) == 1){
                    return e;
                }
            }
        }

        private int Generate_D()
        {
            d = 1;
            while (true)
            {
                if( ( (e * d) % phi_n) == 1){
                    return d;
                }
                d++;
            }
        }
        public List<byte> Encrypt(string message)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(message);
            List<byte> encryptMessage = new List<byte>();
            foreach (var byteMes in asciiBytes)
            {
                encryptMessage.Add((byte)((byte)Math.Pow(byteMes, e) % n));
            }
            return encryptMessage;

        }

        List<byte> Decrypt(List<byte> encryptMessage)
        {
            List<byte> decryptMessage = new List<byte>();
            foreach(var byteMes in encryptMessage)
            {
                decryptMessage.Add((byte)((byte)Math.Pow(byteMes, d) % n));
            }
            return decryptMessage;
        }
        public string PrintMessage(string message)
        {
            List<byte> encryptMessage = new List<byte>();
            List<byte> decryptMessage = new List<byte>();
            encryptMessage = Encrypt(message);
            decryptMessage = Decrypt(encryptMessage);
            StringBuilder messageBuilder = new StringBuilder();
            foreach (var byteMes in decryptMessage)
            {
                messageBuilder.Append(byteMes.ToString());
            }
            return messageBuilder.ToString();
        }
        private int Min(int x, int y)
        {
            return x < y ? x : y;
        }

        private int Max(int x, int y)
        {
            return x > y ? x : y;
        }

        private int GCD(int a, int b)
        {
            if (a == 0)
            {
                return b;
            }
            else
            {
                var min = Min(a, b);
                var max = Max(a, b);
                //вызываем метод с новыми аргументами
                return GCD(max % min, min);
            }
        }
    }
}
