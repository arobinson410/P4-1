using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P4_1
{
    public static class ComManager
    {
        public static void SendPacket(object p, object c, ref bool b)
        {
            while (b)
            {
                (c as ClientHandler).SendMessage(Serialize(p as Player));
                Thread.Sleep(1000);
            }
            Console.WriteLine("Loop Stopped");
        }

        public static string Serialize(Player p)
        {
            string toReturn;

            toReturn = JsonConvert.SerializeObject(p);

            return toReturn;
        }


    }
}
