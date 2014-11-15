using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;

namespace EleksT0
{
    static class FileGetter
    {
        public static List<string> GetFilesRecursive(string b)
        {
            List<string> result = new List<string>();
            Stack<string> stack = new Stack<string>();

            stack.Push(b);
            while (stack.Count > 0)
            {
                string dir = stack.Pop();
                try
                {
                    result.AddRange(Directory.GetFiles(dir, "*.*"));

                    foreach (string dn in Directory.GetDirectories(dir))
                    {
                        if (CanRead(dn))
                            stack.Push(dn);
                        else
                            Console.WriteLine("You don't have access to {0}", dn);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot open directory ({0}}", e.Message);
                }
            }

            return result;
        }

        public static bool CanRead(string path)
        {
            var readAllow = false;
            var readDeny = false;
            var accessControlList = Directory.GetAccessControl(path);
            if (accessControlList == null)
                return false;
            var accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            if (accessRules == null)
                return false;

            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Read & rule.FileSystemRights) != FileSystemRights.Read) continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    readAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    readDeny = true;
            }
            return readAllow && !readDeny;
        }
    }
}
