using Newtonsoft.Json;
using System;
using System.IO;

using System.Runtime.Serialization;

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace P4_1
{
    /// <summary>
    /// This class contains all the information for a player to be presented to the display window
    /// </summary>
    public class Player : INotifyPropertyChanged
    {

        [JsonProperty]
        private string folderPath;
        [JsonProperty]

        private string name;
        [JsonProperty]
        private double x, y;
        [JsonProperty]
        private double[] velocity;

        [JsonProperty]
        private bool _isIt;
        [JsonProperty]
        private Guid uid; 

        private byte[] rgbColor = new byte[3];
        private bool _isIt;
        private Guid uid;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The Player object constructor
        /// </summary>
        /// <param name="StartY">Starting Y position of the Player.</param>
        /// <param name="StartX">Starting X position of the Player.</param>
        public Player(int StartY, int StartX)
        {

            folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\P4";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            Random rand = new Random();


            name = "Player";
            x = StartX;
            y = StartY;
            _isIt = false;

            velocity = new double[]{ 10, 10 };
            uid = Guid.NewGuid();
            rand.NextBytes(rgbColor);
        }
        /// <summary>
        /// In order to make sure that there is only one of each Player rendered, a UID is used.
        /// </summary>

        public String UID
        {
            get
            {
                return uid.ToString();
            }
            set
            {
                uid = Guid.Parse(value);
            }
        }


        /// <summary>
        /// Name of the Player, by default this is player.
        /// </summary>

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }


        /// <summary>
        /// X coordinate for the location of the player.
        /// </summary>
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                if (value < 0)
                    x = 0;
                else
                    x = value;

                OnPropertyChanged("X");
            }
        }


        /// <summary>
        /// Indicates if the player is "it"
        /// </summary>

        public bool isIt
        {
            get
            {
                return _isIt;

            }

            set
            {
                _isIt = value;
            }
        }

        /// <summary>
        /// Y coordinate for the location of the player.
        /// </summary>
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                if (value < 0)
                    y = 0;
                else
                    y = value;

                OnPropertyChanged("Y");
            }
        }


        /// <summary>
        /// A velocity to indicate how far the Player should move every second.
        /// </summary>

        public double[] Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }


        /// <summary>
        /// A color for the main window to bind to. In order for the value to be transmitted to the server, it is not presented as this object, but as an byte array.
        /// </summary>
        [JsonIgnore]
        public SolidColorBrush Color
        {
            get
            {
                return new SolidColorBrush(new Color() { R = rgbColor[0], G = rgbColor[1], B = rgbColor[2], A = (byte)255 });
            }
            set
            {
                rgbColor[0] = value.Color.R;
                rgbColor[1] = value.Color.G;
                rgbColor[2] = value.Color.B;
            }
        }

        /// <summary>
        /// A byte array to send to the server representing the RGB values of the Player's color.
        /// </summary>
        public byte[] RGB
        {
            get
            {
                return rgbColor;
            }
            set
            {
                rgbColor = value;
            }
        }

        /// <summary>.
        /// Called to notify the ViewModel that an attribute has changed
        /// </summary>
        /// <param name="s">A string indicating the name of the attribute.</param>

        private void OnPropertyChanged(string s)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
        }

        /// <summary>
        /// An equality override to test if two players are the same via their UIDs.
        /// </summary>
        /// <param name="obj">Object to compare to</param>
        /// <returns>True if the GUIDs are the same. False otherwise.</returns>
        public override bool Equals(object obj)
        {
            Player p = obj as Player;
            if(p != null)
            {
                if (p.UID == this.UID)
                {
                    return true;
                }
            }

            return false;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
