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

namespace ExtractorFicherosProgramaciónDAM
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

        public void Ejecucion()
        {
            try
            {
                Extractor.BusquedaPrimera(Path_Origen.Text, Path_Destino.Text);
                TextBox_TodoBien.Text = "Programa Completado... Todo Correcto";
                TextBox_TodoBien.Foreground = Brushes.Green;
                TextBox_TodoBien.Opacity = 100;
                System.Windows.MessageBox.Show("Porfavor, no se le olvide comprobar que todos los archivos se hayan copiado correctamente.\n\nMuchas gracias por usar el programa. :3\n\nSi ocurre algún error, se agradeceria que informaseis a los respectivos delvelopers ubicado en la ventana 'Acerca de' en el menú de ayuda.", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                TextBox_TodoBien.Text = "Algo ha salido mal, intentelo de nuevo";
                TextBox_TodoBien.Foreground = Brushes.Red;
                TextBox_TodoBien.Opacity = 100;
            }
        }
        #region Eventos
        private void MiMenu_Instrucciones_Click(object sender, RoutedEventArgs e)
        {
            string ruta = @"..\..\Imagenes\imgInstrucciones\instrucciones1.png";
            Process.Start(ruta);
        }

        private void BotonIniciar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("¿Estas seguro de que has introducido bien las rutas de los archivos?", "!Atención¡", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Ejecucion();
                    break;
                case MessageBoxResult.No:
                    break;

            }
        }

        private void BotonExaminar_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialogo = new FolderBrowserDialog();
            if (e.OriginalSource == BotonExaminar_Origen)
            {
                dialogo.ShowDialog();
                Path_Origen.Text = dialogo.SelectedPath;
                if (Path_Origen.Text == "")
                {
                    Path_Origen.Text = "Path";
                }
            }
            //Si el boton pulsado es el de la ruta Destino
            if (e.OriginalSource == BotonExaminar_Destino)
            {
                dialogo.ShowDialog();
                Path_Destino.Text = dialogo.SelectedPath;
                if (Path_Destino.Text == "")
                {
                    Path_Destino.Text = "Path";
                }
            }
            //Si las se indico alguna ruta
            if (Path_Destino.Text != "Path" && Path_Origen.Text != "Path")
            {
                BotonIniciar.IsEnabled = true;
            }
            else
            {
                BotonIniciar.IsEnabled = false;
            }
            dialogo.Dispose();
        }

        private void MiMenu_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MiMenu_Acerca_Click(object sender, RoutedEventArgs e)
        {
            AcercaDe ventana = new AcercaDe();
            ventana.ShowDialog();
        }

        private void Version_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Path_Origen.IsEnabled && Path_Destino.IsEnabled)
            {
                Path_Destino.IsEnabled = false;
                Path_Origen.IsEnabled = false;
            }
            else
            {
                Path_Origen.IsEnabled = true;
                Path_Destino.IsEnabled = true;
            }
        }

        private void MiMenu_Git_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == MiMenu_Git_Pablo)
                Process.Start("https://github.com/rasky0607/");
            if (e.OriginalSource == MiMenu_Git_Adrian)
                Process.Start("https://github.com/ElAdrixHD/");
        }
        #endregion
    }
}
