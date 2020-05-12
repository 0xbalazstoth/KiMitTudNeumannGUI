using System;
using System.Collections.Generic;
using System.IO;
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

namespace VersenyzoGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int pontokSzama { get; set; }
        public string megadottPontok { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtBoxPontok_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var count = txtBoxPontok.Text.Split(' ').Count();
                megadottPontok = txtBoxPontok.Text;
                lblDb.Content = count + " db";
                string[] pontok = txtBoxPontok.Text.Trim().Split();
                pontokSzama = pontok.Length;
                int max = Convert.ToInt32(pontok[0]);
                int min = Convert.ToInt32(pontok[0]);
                int osszPont = 0;
                for (int i = 0; i < pontok.Length; i++)
                {
                    if (max < Convert.ToInt32(pontok[i]))
                        max = Convert.ToInt32(pontok[i]);
                    else if (min > Convert.ToInt32(pontok[i]))
                        min = Convert.ToInt32(pontok[i]);
                    osszPont += Convert.ToInt32(pontok[i]);
                }
                lblMax.Content = max;
                lblMin.Content = min;
                if (pontok.Length > 3)
                    lblOssz.Content = osszPont;
            }
            catch (Exception) { }
        }

        private void btnHozzaad_Click(object sender, RoutedEventArgs e)
        {
            int letezik = 0;
            using (StreamReader olvas = new StreamReader(@"selejtezo.txt"))
            {
                while (!olvas.EndOfStream)
                {
                    string[] split = olvas.ReadLine().Split(';');
                    string nev = split[0];
                    if (nev == txtBoxNev.Text)
                        letezik++;
                }
            }
            if (letezik == 1)
                MessageBox.Show("Van már ilyen nevű versenyző!");
            if (pontokSzama < 6)
                MessageBox.Show("A pontszámok száma nem megfelelő!");
            if (letezik == 0 && pontokSzama >= 6)
            {
                MessageBox.Show("Az állomány bővítése sikeres volt!");
                FileStream fajl = new FileStream(@"selejtezo.txt", FileMode.Append);
                StreamWriter ki = new StreamWriter(fajl, Encoding.UTF8);
                ki.WriteLine($"{txtBoxNev.Text};{megadottPontok}");
                ki.Flush();
                ki.Close();
                fajl.Close();
                txtBoxNev.Clear();
                txtBoxPontok.Clear();
                lblDb.Content = "";
                lblMax.Content = "";
                lblMin.Content = "";
                lblOssz.Content = "";
            }
        }
    }
}
