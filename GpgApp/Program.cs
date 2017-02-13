using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Gpg.NET;

namespace GpgApp
{
	class Program
	{
		static void Main(string[] args)
		{
			GpgNet.Initialise(@"C:\Program Files (x86)\GnuPG\bin\libgpgme-11.dll");
			Console.WriteLine($"Started GpgME version {GpgNet.Version}");

			Test0();
			//Test1();
			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();

			while (true)
			{
				Thread.Sleep(500);
			}
		}

		private static void Test1()
		{
			var context = GpgContext.CreateContext();
			var recipient = context.FindKey("dev");

			var reencFile = Environment.ExpandEnvironmentVariables(@"%userprofile%\.password-store\testpw-reenc.gpg");
			var file = MemoryGpgBuffer.CreateFromFile(reencFile);
			var plaintext = new StreamReader(context.Decrypt(file)).ReadToEnd();
			Console.WriteLine(plaintext);
		}

		private static void Test0()
		{
			// Create a new GPG context
			var context = GpgContext.CreateContext();

			context.KeylistMode = context.KeylistMode | GpgKeylistMode.WithSecret;

			// Print GPG keys
			var keys = context.FindKeys().ToArray();
			foreach (var key in keys)
			{
				Console.WriteLine(key.ToString());
				Console.WriteLine($"\t{key.Uids.First()}");
				foreach (var subkey in key.Subkeys)
				{
					Console.WriteLine($"\t{subkey}");
				}
			}

			var passwordFile = Environment.ExpandEnvironmentVariables(@"%userprofile%\.password-store\testpw.gpg");
			// Create a GPG data buffer for storing the ciphertext
			var inputBuffer = MemoryGpgBuffer.CreateFromFile(passwordFile);

			var content = new StreamReader(context.Decrypt(inputBuffer)).ReadToEnd();

			Console.WriteLine("Decryption output:");
			Console.WriteLine(content);

			var result = context.Encrypt(MemoryGpgBuffer.CreateFromString(content + "Re-Encrypted: True\n"), keys.Take(1));

			var reEncPasswordFile = Environment.ExpandEnvironmentVariables(@"%userprofile%\.password-store\testpw-reenc.gpg");
			using (var fs = File.OpenWrite(reEncPasswordFile))
			{
				result.CopyTo(fs);
			}
			Console.WriteLine("File was re-encrypted successfully.");
		}
	}
}
