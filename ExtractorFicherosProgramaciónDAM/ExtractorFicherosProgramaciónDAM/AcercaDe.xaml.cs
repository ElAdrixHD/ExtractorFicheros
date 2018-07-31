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
    /// Lógica de interacción para AcercaDe.xaml
    /// </summary>
    public partial class AcercaDe : Window
    {
        public AcercaDe()
        {
            InitializeComponent();
        }

        private void Cerrar_AcercaDe()
        {
            this.Close();
        }

        private void Boton_AcercaDe_Cerrar_Click(object sender, RoutedEventArgs e)
        {
            Cerrar_AcercaDe();
        }
    }
}
