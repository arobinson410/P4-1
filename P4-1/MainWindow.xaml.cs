using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P4_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Thread comThread;
        bool b_ClientSendMessage = true;

        ClientHandler c;
        ObservableCollection<Player> players = new ObservableCollection<Player>();

        Random rand = new Random((int)DateTime.Now.Ticks);
        public Player player;
        bool firstRun = false;

        /// <summary>
        /// Entry point of Client program.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();


            
            player = new Player(rand.Next(0, (int)this.Width),rand.Next(0,(int)this.Height));

            c = new ClientHandler("127.0.0.1", 13000, player.Name, PlayersUpdate);
            comThread = new Thread(_ => ComManager.SendPacket(player, c, ref b_ClientSendMessage));
            Thread.Sleep(1000);
            comThread.Start();

            

            ContentPresenterVM contentPresenterVM = new ContentPresenterVM(players);
            contentPresenterVM.AddElement(player);

            this.Display.DataContext = contentPresenterVM;
        }

        /// <summary>
        /// Update the Player list with information received from other clients.
        /// </summary>
        /// <param name="s">Serialized json string of Player</param>
        private void PlayersUpdate(string s)
        {

            Player recieved = JsonConvert.DeserializeObject<Player>(s);


            int index = CheckForExist(recieved);
            if (index == -1)
            {
                players.Add(recieved);
            }
            else
            {
                players.RemoveAt(index);
                players.Add(recieved);
            }

            // Go through each player and assign one to it if there isn't already one that is "it
            bool playerIsIt = false;
            int itIndex = -1;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].isIt)
                {
                    playerIsIt = true;
                    itIndex = i;
                }
            }

            if (!playerIsIt)
            {
                players[0].isIt = true;
                itIndex = 0;
            }

            for (int i = 1; i < players.Count; i++)
            {
                if (players[i].isIt && Math.Abs(players[i].X - player.X) < 10 && Math.Abs(players[i].Y - player.Y) < 10)
                {
                    
                    this.Close();
                }
            }

            
        }

        /// <summary>
        /// Ensures the player is not already in the list.
        /// </summary>
        /// <param name="p">Player object</param>
        /// <returns>Index in player list if the Player is in the list, -1 otherwise.</returns>
        private int CheckForExist(Player p)
        {
            for(int i = 0; i < players.Count; i++){
                if (p.UID.Equals(players[i].UID))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Button event callback for the arrow press
        /// </summary>
        /// <param name="sender">Send object of the event</param>
        /// <param name="e">Event argument of the keypress event</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Window source = sender as Window;
            if(source != null)
            {
                switch (e.Key)
                {
                    case Key.Up:
                        player.X -= player.Velocity[0];
                        break;
                    case Key.Down:
                        player.X += player.Velocity[0];
                        break;
                    case Key.Left:
                        player.Y -= player.Velocity[1];
                        break;
                    case Key.Right:
                        player.Y += player.Velocity[1];
                        break;
                }
            }
        }

        /// <summary>
        /// Cleanup method on window close.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Argument of the window closing</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            b_ClientSendMessage = false;
            Thread.Sleep(100);
            c.StopClient();
        }
    }
}
