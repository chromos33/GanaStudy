using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationtest
{
    class Program
    {
        static void Main(string[] args)
        {
            string nick = "_HavoK";

            while (nick.EndsWith("_"))
            {
                nick = nick.Remove(nick.Length - 1);
            }
            Console.WriteLine(nick);
            while (nick.StartsWith("_"))
            {
                Console.WriteLine(nick);
                nick = nick.Remove(0,1);
                Console.WriteLine("-");
                Console.WriteLine(nick);
            }
            
            if (nick.EndsWith("afk"))
            {
                nick = nick.Replace("afk", "");
            }
            if (nick.EndsWith("handy"))
            {
                nick = nick.Replace("handy", "");
            }
            if (nick.Contains("_"))
            {
                nick = nick.Split('_')[0];
            }
            if (nick.Contains("|"))
            {
                nick = nick.Split('|')[0];
            }
            string lastchar = "f";
            string secondlastchar = "f";
            try
            {
                lastchar = nick.Substring(nick.Length - 1, 1);
                secondlastchar = nick.Substring(nick.Length - 2, 1);
            }
            catch (Exception)
            {

            }

            int n;
            int m;
            if (int.TryParse(lastchar, out n) && !int.TryParse(secondlastchar, out m))
            {
                nick = nick.Substring(0, nick.Length - 1);
            }
            if (nick == "")
            {
                nick = "error";
            }
            Console.WriteLine(nick);
            Console.ReadKey();
        }
    }
}
