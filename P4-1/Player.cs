using Newtonsoft.Json;
using System;
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
        private string name;
        private double x, y;
        private double[] velocity;
        private Guid uid; 

        public event PropertyChangedEventHandler PropertyChanged;

        public Player()
        {
            name = "Player";
            x = 0;
            y = 0;
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
    }
}
