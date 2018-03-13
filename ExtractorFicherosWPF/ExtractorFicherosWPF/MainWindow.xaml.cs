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
        static  bool  saliobien = true;
        public static bool sacaFicherosDll = false;//Cuando esta marcada la obcion de obtener solo los .cs y .exe
        SolidColorBrush colorPorDefecto = new SolidColorBrush(Colors.Gray);
      

        public MainWindow()
        {
            InitializeComponent();
            BarraProgreso.Foreground =colorPorDefecto;

        }

        #region Metodos    
        /// <EjecucionInfo>
        ///  LLama a todas las clases implicadas en el programa para una ejecuccion de este.
        /// </EjecucionInfo>
        private void Ejecucion()
        {                 
                saliobien = Fichero.LecturaFicheros();

                Fichero.LecturaFicheroExe();//POR AQUI**

            TextBox_TodoBien.Opacity = 100;
                Path_Origen.Text = "Path";
                Path_Destino.Text = "Path";
                BotonIniciar.IsEnabled = false;
            if (saliobien == true)
            {
                SolidColorBrush colorCorrecto = new SolidColorBrush(Colors.DarkGreen);
                TextBox_TodoBien.Foreground = colorCorrecto;
                TextBox_TodoBien.Text = "Proceso completado satisfactoriamente!.";
                BarraProgreso.Foreground = colorCorrecto;
                BarraProgreso.Value = BarraProgreso.Maximum;
            }
            if (saliobien == false)
            {
                SolidColorBrush colorerror = new SolidColorBrush(Colors.DarkRed);
                TextBox_TodoBien.Foreground = colorerror;
                TextBox_TodoBien.Text = "¡ERROR!,Proceso no completado. Comprueba las rutas por favor.";
                BarraProgreso.Foreground = colorerror;
                BarraProgreso.Value = BarraProgreso.Maximum/1.5;
            }
        }

        public static bool MensajeError(Exception ex)
        {
           return saliobien = false;               
        }

        #endregion

        #region Metodos de Eventos Click

        /// <CerrarAppInfo>
        /// Cierra la aplicación
        /// </CerrarAppInfo>
        private void CerrarApp()
        {
            App.Current.Shutdown();
        }
        private void MiMenu_Salir_Click(object sender, RoutedEventArgs e)
        {
            CerrarApp();
        }

        /// <VentanaAcercaDe_AbrirInfo>
        /// Abre la ventana "Acerca de"
        /// </VentanaAcercaDe_AbrirInfo>
        private void VentanaAcercaDe_Abrir()
        {
            VentanaAcercaDe_Principal ventanaAcercaDe = new VentanaAcercaDe_Principal();
            ventanaAcercaDe.ShowDialog();
        }
        private void MiMenu_Acerca_Click(object sender, RoutedEventArgs e)
        {       
            VentanaAcercaDe_Abrir();
        }

        /// < MiMenu_Git_ClickInfo>
        /// Redirige a los Git de los  desarrolladores
        /// </ MiMenu_Git_ClickInfo>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiMenu_Git_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == MiMenu_Git_Pablo)
                Process.Start("https://github.com/rasky0607/");
            if(e.OriginalSource == MiMenu_Git_Adrian)
                Process.Start("https://github.com/ElAdrixHD/");
        }//Refactorizado Listo    

        /// <BotonExaminar_ClickInfo>
        /// Recoge las rutas y las muestra en el texbox al usuario
        /// </BotonExaminar_ClickInfo>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonExaminar_Click(object sender, RoutedEventArgs e)//Refactorizado Listo 
        {
            BarraProgreso.Foreground = colorPorDefecto;//restablece el color de inicio a la barra de progreso
            //Limpieza texbox de mensajes de error y completo
            TextBox_TodoBien.Text = " ";
            //-------------------------------------------
            Directorio miDirectorio = new Directorio();
            //Si el boton pulsado es el de la ruta Origen
            if (e.OriginalSource == BotonExaminar_Origen)
            {
                Path_Origen.Text = Directorio.CargarRutaOrigen();
                if (Path_Origen.Text == "")
                {
                    Path_Origen.Text = "Path";
                }
            }
            //Si el boton pulsado es el de la ruta Destino
            if (e.OriginalSource == BotonExaminar_Destino)
            {

                Path_Destino.Text = Directorio.CargarRutaDestino();
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
        }

        /// <BotonIniciar_ClickInfo>
        /// Redirige al metodo Ejecucion del programa para llevar a cabo la cadena de llamadas a las otras clases y metodos.
        /// </BotonIniciar_ClickInfo>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <rbConDll_CheckedInfo>
        /// Opcion para Extraer tambien los ficheros DLL de los proyectos, ademas el los .exe y . cs
        /// </rbConDll_CheckedInfo>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbConDll_Checked(object sender, RoutedEventArgs e)
        {
            sacaFicherosDll = true;
        }

        /// <Version_MouseEnterInfo>
        /// Habilita y deshabilita la el texbox donde se colocaran las rutas, para permitir la escritura manual o no
        /// </Version_MouseEnterInfo>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #endregion

        /// <MiMenu_Instrucciones_Click>
        /// Abre el editor de imagenes que se tenga configurado por defecto el cual cargara las  imagenes de la carpeta de instrucciones del proyecto
        /// </MiMenu_Instrucciones_Click>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiMenu_Instrucciones_Click(object sender, RoutedEventArgs e)
        {
            string ruta = @"..\..\Imagenes\imgInstrucciones\instrucciones1.png";
            Process.Start(ruta);

            
        }
    }
}
