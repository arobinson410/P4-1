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

        public Player player;

        public MainWindow()
        {
            player = new Player();
  
            c = new ClientHandler("127.0.0.1", 13000, player.Name, PlayersUpdate);


            comThread = new Thread(_ => ComManager.SendPacket(player, c, ref b_ClientSendMessage));
            comThread.Start();

            InitializeComponent();

            ContentPresenterVM contentPresenterVM = new ContentPresenterVM(players);
            contentPresenterVM.AddElement(player);

            this.Display.DataContext = contentPresenterVM;
        }

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
        }

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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            b_ClientSendMessage = false;
            Thread.Sleep(100);
            c.StopClient();
        }
    }
}
