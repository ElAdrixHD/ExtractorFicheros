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
using System.Diagnostics; //Este espacio de nombre puede abrir procesos para abrir paginas webs (github)
using Microsoft.Win32;
using System.Windows.Forms;

namespace ExtractorFicherosWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Fichero mifichero = new Fichero();
        Directorio miDirectorio = new Directorio();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        

        private string RutaDirectorioOrigen;

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
/*
        /// <summary>
        /// Abre un cuadro para seleccionar la carpeta que desea buscar
        /// </summary>
        /// <returns>Devuelve la ruta del directorio</returns>
        private string AbrirDialogo()
        {
            string path = string.Empty;
            FolderBrowserDialog dialogoDirectorio = new FolderBrowserDialog();
            DialogResult resultado;

            try
            {
                dialogoDirectorio.ShowNewFolderButton = true;
                resultado = dialogoDirectorio.ShowDialog();
                path = dialogoDirectorio.SelectedPath;
                dialogoDirectorio.Dispose();
            }
            catch (Exception)
            {
                
                throw;
            }
            return path;
        }*/

        /// <summary>
        /// Ejecuta el programa
        /// </summary>
        private void Ejecucion()
        {
            bool saliobien;
            
            do
            {
                saliobien = mifichero.LecturaFicheros();
            } while (saliobien == false);
            mifichero.LecturaFicheroExe();

            System.Windows.MessageBox.Show("Programa Completado","Todo Perfecto",MessageBoxButton.OK);
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

        private void BotonExaminar_Origen_Click(object sender, RoutedEventArgs e)
        {
           
            string path;
            path = miDirectorio.AbrirDialogo();
            RutaDirectorioOrigen = path;
            Path_Origen.Text = path;
            
            if (Path_Origen.Text == "")
            {
                Path_Origen.Text = "Path";
            }
        }

        private void BotonExaminar_Destino_Click(object sender, RoutedEventArgs e)
        {
          
            Path_Destino.Text = miDirectorio.AbrirDialogo();
            if (Path_Origen.Text == "")
            {
                Path_Destino.Text = "Path";
            }
        }

        private void BotonIniciar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("¿Estas seguro que has introducido bien las rutas de los archivos?", "Peligro", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Ejecucion();
                    break;
                
            }
        }
    }
}
