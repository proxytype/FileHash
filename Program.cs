using System;
using System.Security.Cryptography;
using System.IO;

namespace FileHasher
{
    class Program
    {
        public enum SUPPORTED_ALGORITHMS
        {
            MD5,
            SHA1,
            SHA256,
            SHA384,
            SHA512,
            ALL
        }

        public const int MIN_ARGS = 2;
        public const int MAX_ARGS = 3;

        static void printHeader()
        {
            Console.WriteLine(" ______ _ _      _    _           _      ");
            Console.WriteLine("|  ____(_) |    | |  | |         | |     ");
            Console.WriteLine("| |__   _| | ___| |__| | __ _ ___| |__   ");
            Console.WriteLine(@"|  __| | | |/ _ \  __  |/ _` / __| '_ \ ");
            Console.WriteLine(@"| |    | | |  __/ |  | | (_| \__ \ | | |");
            Console.WriteLine(@"|_|    |_|_|\___|_|  |_|\__,_|___/_| |_|");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("by: RudeNetworks.com | version: 0.1 beta");
        }

        static void printArgs()
        {
            Console.WriteLine("usage: filehash.exe <filename> <algorithm>");
            Console.WriteLine("supported algorithms:");
            Console.WriteLine(" - " + SUPPORTED_ALGORITHMS.ALL.ToString() + " <print all algorithms outputs>");
            Console.WriteLine(" - " + SUPPORTED_ALGORITHMS.SHA1.ToString());
            Console.WriteLine(" - " + SUPPORTED_ALGORITHMS.SHA256.ToString());
            Console.WriteLine(" - " + SUPPORTED_ALGORITHMS.SHA384.ToString());
            Console.WriteLine(" - " + SUPPORTED_ALGORITHMS.SHA512.ToString());
            Console.WriteLine(" - " + SUPPORTED_ALGORITHMS.MD5.ToString());
        }

        static void Main(string[] args)
        {
            try
            {
                printHeader();

                if (args.Length < MIN_ARGS)
                {
                    printArgs();
                    return;
                }

                string filename = args[0];
                string algorithm = args[1].ToUpper();
                string logfile = string.Empty;

                if (args.Length > MIN_ARGS)
                {
                    logfile = args[2];
                }

                if (!File.Exists(filename))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("file not found!");
                    return;
                }

                SUPPORTED_ALGORITHMS alg;

                if (!Enum.TryParse(algorithm, out alg))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("algorithm not found!");
                    return;
                }

                Console.WriteLine("File:" + filename);
                if (logfile == string.Empty)
                {
                    Console.WriteLine(" * log file disabled.");
                }
                else
                {
                    Console.WriteLine(" * log file enabled.");
                    Console.WriteLine("   - " + logfile);
                }

                Console.WriteLine();
                using (FileStream stream = File.OpenRead(filename))
                {

                    switch (alg)
                    {
                        case SUPPORTED_ALGORITHMS.MD5:
                            Console.WriteLine(" " + SUPPORTED_ALGORITHMS.MD5.ToString() + ": " + getHash(stream, SUPPORTED_ALGORITHMS.MD5));
                            break;
                        case SUPPORTED_ALGORITHMS.SHA1:
                            Console.WriteLine(" " + SUPPORTED_ALGORITHMS.SHA1.ToString() + ": " + getHash(stream, SUPPORTED_ALGORITHMS.SHA1));
                            break;
                        case SUPPORTED_ALGORITHMS.SHA256:
                            Console.WriteLine(SUPPORTED_ALGORITHMS.SHA256.ToString() + ": " + getHash(stream, SUPPORTED_ALGORITHMS.SHA256));
                            break;
                        case SUPPORTED_ALGORITHMS.SHA384:
                            Console.WriteLine(SUPPORTED_ALGORITHMS.SHA384.ToString() + ": " + getHash(stream, SUPPORTED_ALGORITHMS.SHA384));
                            break;
                        case SUPPORTED_ALGORITHMS.SHA512:
                            Console.WriteLine(SUPPORTED_ALGORITHMS.SHA512.ToString() + ": " + getHash(stream, SUPPORTED_ALGORITHMS.SHA512));
                            break;
                        case SUPPORTED_ALGORITHMS.ALL:
                            Console.WriteLine(SUPPORTED_ALGORITHMS.MD5.ToString() + ": " + getHash(stream, SUPPORTED_ALGORITHMS.MD5));
                            Console.WriteLine(SUPPORTED_ALGORITHMS.SHA1.ToString() + ": " + getHash(stream, SUPPORTED_ALGORITHMS.SHA1));
                            Console.WriteLine(SUPPORTED_ALGORITHMS.SHA256.ToString() + ": " + getHash(stream, SUPPORTED_ALGORITHMS.SHA256));
                            Console.WriteLine(SUPPORTED_ALGORITHMS.SHA384.ToString() + ": " + getHash(stream, SUPPORTED_ALGORITHMS.SHA384));
                            Console.WriteLine(SUPPORTED_ALGORITHMS.SHA512.ToString() + ": " + getHash(stream, SUPPORTED_ALGORITHMS.SHA512));
                            break;
                        default:
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
            }

        }

        static string getHash(Stream stream, SUPPORTED_ALGORITHMS alg)
        {
            using (HashAlgorithm hasher = HashAlgorithm.Create(alg.ToString()))
            {
                byte[] hash = hasher.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

    }
}
