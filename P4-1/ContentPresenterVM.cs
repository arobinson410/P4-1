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
    /// <summary>
    /// ViewModel for the display window
    /// </summary>
    public class ContentPresenterVM
    {
        private ObservableCollection<Player> list;

        /// <summary>
        /// Constructor for the ViewModel
        /// </summary>
        /// <param name="l">List of player</param>
        public ContentPresenterVM(ObservableCollection<Player> l)
        {
            list = l;
        }
        /// <summary>
        /// Seperate method to add elements in case additional logic is needed
        /// </summary>
        /// <param name="p"></param>
        public void AddElement(Player p)
        {
            list.Add(p);
        }
        /// <summary>
        /// List of player objects accessible for binding
        /// </summary>
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
