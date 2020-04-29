using Microsoft.VisualStudio.TestTools.UnitTesting;
using P4_1;
using P4_Server;
using Newtonsoft.Json;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void ConnectionTest()
        {
            ServerHandler.Start(13000);
            ServerHandler.BeginAcceptConnections();
            ClientHandler client = new ClientHandler("127.0.0.1", 13000, "test", PlayersUpdate);
        }

        /// <summary>
        /// Dummy function 
        /// </summary>
        /// <param name="s"></param>
        private void PlayersUpdate(string s)
        {
        }
      

        [TestMethod]
        public void PlayerStorage()
        {
            Player p = new Player(0, 0);
            SaveToFile(p);
        }

        public static void SaveToFile(Player player)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\P4";
            string fileName = "\\" + player.UID + ".txt";

            JsonSerializer ser = new JsonSerializer();
            StreamWriter sw = new StreamWriter(folderPath + fileName);
            JsonWriter jw = new JsonTextWriter(sw);

            //Run the file saving on a different thread
            Task.Run(() => ser.Serialize(jw, player));
        }
    }
}
