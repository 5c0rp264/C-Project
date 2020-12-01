using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptoSoft
{
    class Program
    {
        static void Main(string[] args)
        {
			Stopwatch sw = new Stopwatch();
			sw.Start();
			try {
				string from = System.IO.File.ReadAllText(args[1]);
				string to = System.IO.File.ReadAllText(args[2]);
				System.IO.File.WriteAllText(to, encryptDecrypt(from));
				sw.Stop();
				Environment.Exit((int)sw.ElapsedMilliseconds);
			}
			catch (Exception e)
            {
				Console.WriteLine(e);
				Environment.Exit(-1);
            }
			
		}


		private static string encryptDecrypt(string input)
		{
			string pathToKey = @"./key.txt";
			if ( !File.Exists(pathToKey) )
            {
				System.IO.File.WriteAllText(pathToKey, GetUniqueKey(264));
			}
			char[] key = System.IO.File.ReadAllText(pathToKey).ToCharArray();
			char[] output = new char[input.Length];

			for (int i = 0; i < input.Length; i++)
			{
				output[i] = (char)(input[i] ^ key[i % key.Length]);
			}

			return new string(output);
		}


		internal static readonly char[] chars =
			"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

		public static string GetUniqueKey(int size)
		{
			byte[] data = new byte[4 * size];
			using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
			{
				crypto.GetBytes(data);
			}
			StringBuilder result = new StringBuilder(size);
			for (int i = 0; i < size; i++)
			{
				var rnd = BitConverter.ToUInt32(data, i * 4);
				var idx = rnd % chars.Length;

				result.Append(chars[idx]);
			}

			return result.ToString();
		}

	}
}
