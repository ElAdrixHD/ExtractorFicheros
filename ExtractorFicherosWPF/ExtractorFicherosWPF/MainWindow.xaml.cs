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
//Añadido
using System.IO;
using System.Diagnostics;

namespace ExtractorFicherosWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Metodos
        /// <summary>
        /// Cierra la aplicación
        /// </summary>
        private void CerrarApp()
        {
            App.Current.Shutdown();
        }

        /// <summary>
        /// Abre la ventana "Acerca de"
        /// </summary>
        private void VentanaAcercaDe_Abrir()
        {
            VentanaAcercaDe_Principal ventanaAcercaDe = new VentanaAcercaDe_Principal();
            ventanaAcercaDe.ShowDialog();
        }

        /// <summary>
        /// Abre el GitHub de Pablo
        /// </summary>
        private void AbrirGit_Pablo()
        {
            Process.Start("https://github.com/rasky0607/");
        }

        /// <summary>
        /// Abre el GitHub de Adrian
        /// </summary>
        private void AbrirGit_Adrian()
        {
            Process.Start("https://github.com/ElAdrixHD/");
        }

        #endregion

        private void MiMenu_Salir_Click(object sender, RoutedEventArgs e)
        {
            CerrarApp();
        }

        private void MiMenu_Acerca_Click(object sender, RoutedEventArgs e)
        {
            VentanaAcercaDe_Abrir();
        }

        private void MiMenu_Git_Pablo_Click(object sender, RoutedEventArgs e)
        {
            AbrirGit_Pablo();
        }

        private void MiMenu_Git_Adrian_Click(object sender, RoutedEventArgs e)
        {
            AbrirGit_Adrian();
        }
    }
}
