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
using System.Threading;

namespace ExtractorFicherosWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Fichero mifichero = new Fichero();
        static  bool  saliobien = true;
       
        public MainWindow()
        {
            InitializeComponent();
            
        }

        #region Metodos
        /// <CerrarAppInfo>
        /// Cierra la aplicación
        /// </CerrarAppInfo>
        private void CerrarApp()
        {
            App.Current.Shutdown();
        }

        /// <VentanaAcercaDe_AbrirInfo>
        /// Abre la ventana "Acerca de"
        /// </VentanaAcercaDe_AbrirInfo>
        private void VentanaAcercaDe_Abrir()
        {
            VentanaAcercaDe_Principal ventanaAcercaDe = new VentanaAcercaDe_Principal();
            ventanaAcercaDe.ShowDialog();
        }

        /// <AbrirGit_PabloInfo>
        /// Abre el GitHub de Pablo en el navegador
        /// </AbrirGit_PabloInfo>
        private void AbrirGit_Pablo()
        {
            Process.Start("https://github.com/rasky0607/");
        }

        /// <AbrirGit_AdrianInfo>
        /// Abre el GitHub de Adrian en el navegador
        /// </AbrirGit_AdrianInfo>
        private void AbrirGit_Adrian()
        {
            Process.Start("https://github.com/ElAdrixHD/");
            
        }

        /// <EjecucionInfo>
        ///  LLama a todas las clases implicadas en el programa para una ejecuccion de este.
        /// </EjecucionInfo>
        private void Ejecucion()
        {      
                saliobien = mifichero.LecturaFicheros();

                mifichero.LecturaFicheroExe();

                TextBox_TodoBien.Opacity = 100;
                Path_Origen.Text = "Path";
                Path_Destino.Text = "Path";
                BotonIniciar.IsEnabled = false;
            if (saliobien == true)
            {
                SolidColorBrush colorCorrecto = new SolidColorBrush(Colors.DarkGreen);
                TextBox_TodoBien.Foreground = colorCorrecto;
                TextBox_TodoBien.Text = "Proceso completado satifactoriamente!.";
               
            }
            if (saliobien == false)
            {
                SolidColorBrush colorerror = new SolidColorBrush(Colors.DarkRed);
                TextBox_TodoBien.Foreground = colorerror;
                TextBox_TodoBien.Text = "¡ERROR!,Proceso no completado. Comprueba las rutas por favor.";               

            }
        }

        public static bool MensajeError(Exception ex)
        {

          return  saliobien = false;     
            
        }

        #endregion

        #region Metodos de Eventos Click
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
            //Limpieza texbox de mensajes de error y completo
            TextBox_TodoBien.Text = " ";
            //-------------------------------------------

            Directorio miDirectorio = new Directorio();
            Path_Origen.Text = miDirectorio.CargarRutaOrigen();

            if (Path_Origen.Text == "")
            {
                Path_Origen.Text = "Path";
            }
            if (Path_Destino.Text != "Path" && Path_Origen.Text != "Path")
            {
                BotonIniciar.IsEnabled = true;
            }
            else
            {
                BotonIniciar.IsEnabled = false;
            }
        }

        private void BotonExaminar_Destino_Click(object sender, RoutedEventArgs e)
        {
            //Limpieza texbox de mensajes de error y completo
            TextBox_TodoBien.Text = " ";
            //-------------------------------------------

            Directorio miDirectorio = new Directorio();
            Path_Destino.Text = miDirectorio.CargarRutaDestino();
            if (Path_Destino.Text == "")
            {
                Path_Destino.Text = "Path";
            }
            if (Path_Origen.Text != "Path" && Path_Destino.Text != "Path")
            {
                BotonIniciar.IsEnabled = true;
            }
            else
            {
                BotonIniciar.IsEnabled = false;
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
        #endregion
    }
}
