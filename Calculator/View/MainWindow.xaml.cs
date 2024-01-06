using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Calculator.View
{
    /// <summary>
    /// Логика взаимодействия для StartedWindow.xaml
    /// </summary>
    public partial class StartedWindow : Window
    {
        public StartedWindow()
        {
            InitializeComponent();
            MainFrame.Content = new FrCasualCalculator();
        }
    }
}
