using kwadrat.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kwadrat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Kwadrat kwadrat;
        
        public MainWindow()
        {
            InitializeComponent();
            this.kwadrat = new Kwadrat(rectWrap, 0, (SolidColorBrush)new BrushConverter().ConvertFromString("black"), 0, txtPole, txtObwod, lblTest, opacitySlider);
        }


        private void clearText()
        {
            txtBok.Text = String.Empty;
            txtObwod.Text = String.Empty;
            txtPole.Text = String.Empty;
            lblKomunikat.Content = String.Empty;
            rectWrap.Points.Clear();
        }
        private void clearAll()
        {
            clearText();
            rectWrap.Points.Clear();
        }

        private void txtBok_TextChanged(object sender, TextChangedEventArgs e)
        {
            double bok;
            if (double.TryParse(txtBok.Text, out bok) && bok >= 0 && bok <= 400)
            {
                lblKomunikat.Content = String.Empty;
                this.kwadrat?.setSize(bok);
                this.kwadrat?.showParams();
            }
            else
            {
                lblKomunikat.Content = "Wpisz liczbę dodatnią i mniejsza od 400";
            }
        }

        private void btnWyczysc_Click(object sender, RoutedEventArgs e)
        {
            clearAll();
        }

        private void btnRysuj_Click(object sender, RoutedEventArgs e)
        {
            double bok;
            if (double.TryParse(txtBok.Text, out bok) && bok >= 0 && bok <= 400)
            {
                SolidColorBrush color = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbKolory.Text);
                double opacity = (cbPrzezroczysty.IsChecked.Value) ? 0.5 : 1;
                this.kwadrat?.setColor(color);
                this.kwadrat?.setOpacity(opacity);
                this.kwadrat?.draw();
                this.kwadrat?.showParams();
            }
            else
            {
                lblKomunikat.Content = "Brak danych lub zły rozmiar bokud";
            }
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            double bok;
            if (double.TryParse(txtBok.Text, out bok) && bok >= 0 && bok < 400)
            {
                bok++;
                clearAll();
                txtBok.Text = bok.ToString();
                this.kwadrat?.drawSize(bok);
                this.kwadrat?.showParams();
            }
            else
            {
                lblKomunikat.Content = "ZA DUŻO KLIKASZ NA PLUS";
            }
        }
        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            double bok;
            if (double.TryParse(txtBok.Text, out bok) && bok > 0 && bok <= 400)
            {
                bok--;
                clearAll();
                txtBok.Text = bok.ToString();
                this.kwadrat?.drawSize(bok);
                this.kwadrat?.showParams();
            }
            else
            {
                lblKomunikat.Content = "ZA DUŻO KLIKASZ NA MINUS";
            }
        }

        private void radioPokaz_Checked(object sender, RoutedEventArgs e)
        {
            kwadrat?.show(true);
        }
        private void radioUkryj_Checked(object sender, RoutedEventArgs e)
        {
            kwadrat?.show(false);
        }

        private void btnObrot_Click(object sender, RoutedEventArgs e)
        {
            kwadrat?.rotateRight(10);
        }

        private void btnZmniejsz_Click(object sender, RoutedEventArgs e)
        {
            kwadrat.cornerTransform(15);
            this.kwadrat?.showParams();
        }

        private void slide_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double opacity = opacitySlider.Value / 100;
            kwadrat?.setOpacity(opacity);
        }
    }
}
