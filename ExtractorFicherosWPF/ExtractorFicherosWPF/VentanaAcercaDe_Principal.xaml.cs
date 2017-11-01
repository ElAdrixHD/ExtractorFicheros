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

namespace ExtractorFicherosWPF
{
    /// <summary>
    /// Lógica de interacción para VentanaAcercaDe_Principal.xaml
    /// </summary>
    public partial class VentanaAcercaDe_Principal : Window
    {
        public VentanaAcercaDe_Principal()
        {
            InitializeComponent();
        }
        #region Metodos
        private void Cerrar_AcercaDe()
        {
            Ventana_AcercaDe.Close();
        }
        #endregion

        private void Boton_AcercaDe_Cerrar_Click(object sender, RoutedEventArgs e)
        {
            Cerrar_AcercaDe();
        }
    }
}
