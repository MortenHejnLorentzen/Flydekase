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

namespace flydekasser
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ClassFyldekasseBizz CFB = new ClassFyldekasseBizz();
        Window Main = new Window();
        public Window1(object fromWinForm, object fromBiz)
        {
            InitializeComponent();
            Main = (Window)fromWinForm;
            CFB = (ClassFyldekasseBizz)fromBiz;
            if (radButFromDF.IsChecked == true)
            {
                CFB.bolFromDatafile = true;
            }
            if (radButFromDB.IsChecked == true)
            {
                CFB.bolFromDatafile = false;
            }
            if (radButToDF.IsChecked == true)
            {
                CFB.bolToDatafile = true;
            }
            if (radButToDB.IsChecked == true)
            {
                CFB.bolToDatafile = false;
            }
            this.DataContext = (ClassFyldekasseBizz)fromBiz;

        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            ClassMatriale cm = new ClassMatriale((ClassMatriale)this.listView.SelectedItem);
            CFB.UpdateData(textBox.Text, textBox1.Text,cm);
            CFB.SaveData();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ListView lv = this.listView;
            ClassMatriale cm = new ClassMatriale((ClassMatriale)lv.SelectedItem);
            CFB.DeleteData(cm);
            textBox.Text = "";
            textBox1.Text = "0";
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {

            int intThick = 0;

            if (int.TryParse(textBox1.Text, out intThick))
            {
                if (intThick > 0)
                {
                    CFB.AddNewMatriale(textBox.Text, textBox1.Text);
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
            CFB.SaveData();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            
            
            this.Close();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = (ListView)sender;
            ClassMatriale cm = new ClassMatriale((ClassMatriale)lv.SelectedItem);
            textBox.Text = cm.strMatName;
            textBox1.Text = cm.strWight;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            Main.Show();
            
        }

        private void radButFromDF_Checked(object sender, RoutedEventArgs e)
        {
            CFB.SetToFile();
        }

        private void radButFromDB_Checked(object sender, RoutedEventArgs e)
        {
            CFB.SetToDatabase();
        }

        private void radButToDF_Checked(object sender, RoutedEventArgs e)
        {
            CFB.bolToDatafile = true;
        }

        private void radButToDB_Checked(object sender, RoutedEventArgs e)
        {
            CFB.bolToDatafile = false;
        }
    }
}
