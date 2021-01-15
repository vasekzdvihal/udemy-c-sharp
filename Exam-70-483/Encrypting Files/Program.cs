using System;
using System.IO;

namespace Encrypting_Files
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadFile();
            Console.WriteLine("Press enter to encrypt file...");
            Console.ReadLine();

            EncryptFile(@"C:\MLFiles\ml.txt");

            Console.WriteLine("Press enter to decrypt file...");
            Console.ReadLine();

            DecryptFile(@"C:\MLFiles\ml.txt");
        }

        public static void ReadFile()
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line
            System.IO.StreamReader file = new StreamReader(@"C:\MLFiles\ml.txt");

            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }

            counter++;

            file.Close();
        }

        public static void EncryptFile(string x)
        {
            File.Encrypt(x);
        }

        public static void DecryptFile(string x)
        {
            File.Decrypt(x);
        }
    }
}
