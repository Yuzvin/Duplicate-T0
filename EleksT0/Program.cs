using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace EleksT0
{
    class Program
    {
        const long sizeLimit = 2147483648;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the directory:");
            string directory = @"C:\Users\WriterMix\Desktop\Dubs";
            //directory = Console.ReadLine();

            Dictionary<string, string> DictionaryWithoutDubs = new Dictionary<string, string>();
            List<SimilarFiles> arrayOfDubs = new List<SimilarFiles>();
            List<string> filesInDirectories = FileGetter.GetFilesRecursive(directory);


            int countOfOversizedFiles = 0;

            foreach (string CurrentFile in filesInDirectories)
            {
                FileInfo info = new FileInfo(CurrentFile);


                if (info.Length < sizeLimit)
                {
                    string file;
                    if (DictionaryWithoutDubs.TryGetValue(info.Length.ToString(), out file) == false)
                    {
                        string hash = MD5Hash.Hash(info);

                        if (DictionaryWithoutDubs.TryGetValue(hash, out file) == false)
                        {
                            DictionaryWithoutDubs.Add(hash, CurrentFile);
                        }
                        else
                        {
                            SimilarFiles f = new SimilarFiles();
                            f.dub1 = CurrentFile;
                            f.dub2 = file;
                            arrayOfDubs.Add(f);
                        }
                    }
                }
                else
                {
                    countOfOversizedFiles++;
                }

            }
            if (countOfOversizedFiles > 0)
                Console.WriteLine("{0} files in directory are bigger than {1} bytes \n", countOfOversizedFiles, sizeLimit);

            if (arrayOfDubs.Count == 0)
            {
                Console.WriteLine("There's no dublicates found!");
            }
            else
            {
                Console.WriteLine("\nDublicates found!");
                for (int i = 0; i < arrayOfDubs.Count; i++)
                    Console.WriteLine("\nFile 1: {0} \nFile 2: {1} \n\n", arrayOfDubs[i].dub1, arrayOfDubs[i].dub2);
            }
            Console.ReadLine();

        }
    }
}