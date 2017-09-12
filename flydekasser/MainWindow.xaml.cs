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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace flydekasser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClassFyldekasseBizz CFB = new ClassFyldekasseBizz();
        public MainWindow()
        {
            InitializeComponent();
            if (radButDatafile.IsChecked == true)
            {
                CFB.bolFromDatafile = true;
            }
            if(radButDatabase.IsChecked == true)
            {
                CFB.bolFromDatafile = false;
            }
            this.DataContext = CFB;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int intThick = 0;

            if (int.TryParse(textBox.Text, out intThick))
            {
                if (intThick > 0)
                {
                    CFB.ValgAfMat(comboBox, textBox.Text);
                }
                else
                {
                    MessageBox.Show("tal intastet bør være støre ind 0");
                }
            }
            else
            {
                MessageBox.Show("intastet ugyldit tal");
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            decimal decHight = 0.0000M;
            decimal decWidth = 0.0000M;
            decimal decDepth = 0.0000M;
            if(decimal.TryParse(textBox1.Text, out decHight)&&
            decimal.TryParse(textBox2.Text, out decWidth)&&
            decimal.TryParse(textBox3.Text, out decDepth))
            {
                if (decHight > 0 && decDepth > 0 && decWidth > 0)
                {
                    CFB.SetBoxDimen(decHight, decWidth, decDepth);
                }
                else
                {
                    MessageBox.Show("tal intastet bør være støre ind 0");
                }
            }
            else
            {
                MessageBox.Show("intastet ugyldit tal");
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            CFB.MakeReport();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1(this,CFB);
            win1.Show();
            this.Hide();
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void radButDatafile_Checked(object sender, RoutedEventArgs e)
        {
            CFB.SetToFile();
        }

        private void radButDatabase_Checked(object sender, RoutedEventArgs e)
        {
            CFB.SetToDatabase();
        }
    }
}
