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

        public event PropertyChangedEventHandler PropertyChanged;

        public Player(int StartY, int StartX)
        {
            folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\P4";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            name = "Player";
            x = StartX;
            y = StartY;
            _isIt = false;
            velocity = new double[]{ 10, 10 };
            uid = Guid.NewGuid();

            
        }

        
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
        private void OnPropertyChanged(string s)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
        }

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
