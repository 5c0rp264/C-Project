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
				string from = args[1];
				string to = args[2];
				/*string from = "/Users/quentinaoustin/Public/test/crypted.txt";
				string to = "/Users/quentinaoustin/Public/test/uncrypted.txt";
				EncryptDecrypt(from, to);*:
				sw.Stop();
				Environment.Exit((int)sw.ElapsedMilliseconds);
			}
			catch (Exception e)
            {
				sw.Stop();
				Console.WriteLine(e);
				Environment.Exit(-1);
            }
			
		}

        private static void EncryptDecrypt(string sourcepath, string targetpath)
		{
			string pathToKey = @"./key.txt";
			if ( !File.Exists(pathToKey) )
            {
				System.IO.File.WriteAllText(pathToKey, GetUniqueKey(264));
			}
			byte[] key = Encoding.ASCII.GetBytes(System.IO.File.ReadAllText(pathToKey));

			byte[] buffer = new byte[4096];

			FileStream fsSource;
			FileStream fsTarget;

			using (fsSource = new FileStream(sourcepath, FileMode.Open, FileAccess.Read))
			{
				//open writting stream
				using (fsTarget = new FileStream(targetpath, FileMode.OpenOrCreate, FileAccess.Write))
				{
					int bytesRead = 0;

					//read each byte and call the xor method before write them 
					while ((bytesRead = fsSource.Read(buffer, 0, buffer.Length)) > 0)
					{
						fsTarget.Write(xorMeThisPlz(buffer, key), 0, bytesRead);
					}
					//clear buffer and write data in the file
					fsTarget.Flush();
					buffer = null;
				}
			}

			//return new string(output);
		}


		static readonly char[] chars =
			"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

		private static string GetUniqueKey(int size)
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


		private static byte[] xorMeThisPlz(byte[] data, byte[] key)
		{
			/*char[] cryptedData = new char[input.Length];
			for (int i = 0; i < input.Length; i++)
			{
				output[i] = (char)(input[i] ^ key[i % key.Length]);
			}*/

			byte[] cryptedData = new byte[data.Length];

			for (int i = 0; i < data.Length; i++)
			{
				cryptedData[i] = (byte)(data[i] ^ key[i % key.Length]);
			}

			return cryptedData;
		}

	}
}
