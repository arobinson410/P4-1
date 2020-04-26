using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace P4_1
{
    public class ContentPresenterVM
    {
        private ObservableCollection<Player> list;

        public ContentPresenterVM(ObservableCollection<Player> l)
        {
            list = l;
        }

        public void AddElement(Player p)
        {
            list.Add(p);
        }

        public ObservableCollection<Player> List
        {
            get
            {
                return list;
            }
            set
            {
                list = value;
            }
        }
    }
}
