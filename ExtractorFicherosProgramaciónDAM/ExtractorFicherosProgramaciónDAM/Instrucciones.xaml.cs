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

namespace ExtractorFicherosProgramaciónDAM
{
    /// <summary>
    /// Lógica de interacción para Instrucciones.xaml
    /// </summary>
    public partial class Instrucciones : Window
    {
        public Instrucciones()
        {
            InitializeComponent();
        }

        /*private void CambioImagen_Click(object sender, RoutedEventArgs e)
        {
            if ((string)CambioImagen.Content == "1/2")
            {
                imagenInstruccion.Source = new BitmapImage(new Uri("Imagenes/imgInstrucciones/instrucciones2.png", UriKind.Relative));
                CambioImagen.Content = "2/2";
            }
            else
            {
                imagenInstruccion.Source = new BitmapImage(new Uri("Imagenes/imgInstrucciones/instrucciones1.png", UriKind.Relative));
                CambioImagen.Content = "1/2";
            }
            
        }*/
    }
}
