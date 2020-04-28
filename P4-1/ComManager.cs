using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P4_1
{
    /// <summary>
    /// A static class that manages the communications on its own thread.
    /// </summary>
    public static class ComManager
    {
        /// <summary>
        /// This method sends the json serialized Player object to the server.
        /// </summary>
        /// <param name="p">Player object</param>
        /// <param name="c">ClientHandler object</param>
        /// <param name="b">Bool that keeps the client sending loop running</param>
        public static void SendPacket(object p, object c, ref bool b)
        {
            while (b)
            {
                (c as ClientHandler).SendMessage(Serialize(p as Player));
                Thread.Sleep(1000);
            }
            Console.WriteLine("Loop Stopped");
        }
        /// <summary>
        /// Method that handles the json serialization
        /// </summary>
        /// <param name="p">Player object to be deserialized</param>
        /// <returns>A serialized json string</returns>
        public static string Serialize(Player p)
        {
            string toReturn;

            toReturn = JsonConvert.SerializeObject(p);

            return toReturn;
        }


    }
}
