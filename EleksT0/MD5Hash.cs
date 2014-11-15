using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace EleksT0
{
    static class MD5Hash
    {
        static MD5 md5 = MD5.Create();

        static public string Hash(FileInfo file)
        {
            byte[] hashBytes = md5.ComputeHash(file.OpenRead());
            string hash = BitConverter.ToString(hashBytes);
            return hash;
        }
    }
}
