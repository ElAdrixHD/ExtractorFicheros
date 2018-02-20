using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Añadido
using System.IO;
using System.Windows.Forms;

namespace ExtractorFicherosWPF
{
    class Directorio 
    {
        #region variables globales
        
        static string rutaDirectoriosNuevos = string.Empty;//Directorios nuevos donde copiaremos los ficheros. 
        static string rutaParaSubdirectorio = string.Empty;//Ruta hacia la carpeta raiz original donde leeremos todos los subdirectorios originales y ruta hacia donde copiaremos los subdirectorios al extraer los ficheros(ambas cosas) 
        static string[] arryRutasNuevas;//Colecion de rutas nuevas des de el directorio raiz nuevo hasta cada uno de los subdirectorios       
        static bool hayfichero = false;//Comprueba si encontro un fichero o n o en el nivel de directorios que se encuentra.
        static string[] ArrayRutasOriginales;//Rutas originales hasta el directorio mas proximo a los ".cs"
        static string[] Arraynombreejecutables;//Array de cada uno de los nombres de los ejecutables a buscar"ya que podemos encontrar varios .exe en un proyecto".
        static string[] ArrayRutasOriginalesExe;//Array de rutas hasta los ejecutables .exe
        static string[] ArrayNomDirectorioRaizProyecto;//Array del nombre de la carpeta raiz de UN PROYECTO Ej: c.\\Ejercicios\Ej1 coge solo Ej1!
        static string[] ArrayComprobacionRutasOriginales;//Este array se llena de rutas a comprobar de cada proyecto Ej. C:\\Ejercicios\EJ1\App_reloj comprueba  si dentro de esta hay otro directorio llamado igual
        static string rutaOrigen = string.Empty;//Ruta origen donde encontramos todos los Subdirectorios con cada uno de los proyectos
        static string rutaDestino = string.Empty;
        static DirectoryInfo[] directorios;
        static string[] arryRutasNuevasTmp;//Array de rutas donde se han creado cada uno de los nuevos subdirectorios en el nuevo directorio Raiz.  

        #endregion 
        //---------------------------------------------------------------------------------------------------------------------
        public static string AbrirDialogo()
        {
            //string path = string.Empty;
            FolderBrowserDialog dialogoDirectorio = new FolderBrowserDialog();
            DialogResult resultado;
            try
            {
                dialogoDirectorio.ShowNewFolderButton = true;
                resultado = dialogoDirectorio.ShowDialog();
                rutaParaSubdirectorio = dialogoDirectorio.SelectedPath;
               // path = dialogoDirectorio.SelectedPath;
               // rutaParaSubdirectorio = path;
                dialogoDirectorio.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
           // return path;
            return rutaParaSubdirectorio;
        }// Refactorizado Listo

        public static string CargarRutaOrigen()
        {
            return rutaOrigen = AbrirDialogo();
        }// Refactorizado Listo

        public static string CargarRutaDestino()//Pendiente
        {
            return rutaDestino = AbrirDialogo();
        }
        //-----------------------------------------------------------------------------------------------------------------------

      
        /// <Metodo que Lee el Directorio raiz>
        /// Lee el directorio raiz, y monta las rutas hasta los ficheros ".cs"
        /// </summary>
        /// <returns>ArrayRutasOriginales</returns>
        public static string[] LecturaSubDirectorios()//Refactorizado Listo
        {

            string rutatmp = string.Empty; //ruta temporal , para montar la ruta hacia los nuevos directorios que se van a crear iguales que los originale, excepto el raiz.
            DirectoryInfo d = new DirectoryInfo(rutaOrigen);
            directorios = d.GetDirectories();
            ArrayRutasOriginales = new string[directorios.Length];
            ArrayRutasOriginalesExe = new string[ArrayRutasOriginales.Length];
            arryRutasNuevas = new string[directorios.Length];
            arryRutasNuevasTmp = new string[ArrayRutasOriginales.Length];
            Arraynombreejecutables = new string[ArrayRutasOriginales.Length];

            MontaRutaHaciaElSubdirectorio(rutaOrigen);//Refactorizado Listo (Metodo Extraido)    
            CreaNuevosDirectorios();//Refactorizado Listo (Metodo Extraido)                       
            BusquedaDeCsProfunda(); //Refactorizado Listo (Metodo Extraido) 

            return ArrayRutasOriginales;
        }

        /// <MontaRutaHaciaElSubdirectorioInfo>
        /// Monta y guarda todas las rutas desde la Raiz hasta cada uno de los subdirectorios.
        /// </MontaRutaHaciaElSubdirectorioInfo>
        /// <param name="rutaOrigen">ruta origen donde encontramos todo estos subdirectorios</param>
        private static void MontaRutaHaciaElSubdirectorio(string rutaOrigen)
        {
            for (int i = 0; i < directorios.Length; i++)
            {
                ArrayRutasOriginales[i] = rutaOrigen + Path.DirectorySeparatorChar + directorios[i];
            }
        }//Listo

        /// <BusquedaDeCsProfundaInfo>
        /// Busca por cada uno delos directorios dentro del proyecto los ficheros .CS
        /// </BusquedaDeCsProfundaInfo>
        private static void BusquedaDeCsProfunda()
        {
            string subdirectorioanadidofinal = string.Empty;//Variable usada para añadir el ultimo subdirectorio mas profundo antes de llegar a los ficheros 
            for (int i = 0; i < ArrayRutasOriginales.Length; i++)
            {
                string tmp = string.Empty;
                DirectoryInfo directorio2 = new DirectoryInfo(ArrayRutasOriginales[i]);
                DirectoryInfo[] dEncontrados = directorio2.GetDirectories();
                Fichero mifichero = new Fichero();
                do
                {
                    if (dEncontrados[0].ToString() != "")
                    {
                        //Este buble comprueba que en el nivel de la ruta en el que se encuentra hay un fichero ".cs " si no es asi busca otro subdirectorio y avanza hasta encontrarlos.
                        do
                        {
                            if (mifichero.CompruebaFichero(ArrayRutasOriginales[i]) == false)
                            {
                                for (int j = 0; j < dEncontrados.Length; j++)
                                {
                                    subdirectorioanadidofinal = dEncontrados[j].ToString();
                                    if (subdirectorioanadidofinal != ".vs" && subdirectorioanadidofinal != "TestResults" && subdirectorioanadidofinal != ".git" && subdirectorioanadidofinal != "Visual Studio 2012")//Simplificar o reorganizar con patrones..
                                    {

                                        tmp = ArrayRutasOriginales[i];
                                        tmp += Path.DirectorySeparatorChar.ToString() + subdirectorioanadidofinal;
                                        ArrayRutasOriginales[i] = tmp;
                                        hayfichero = mifichero.CompruebaFichero(ArrayRutasOriginales[i]);
                                    }

                                }
                            }
                            else
                                hayfichero = true;
                        } while (hayfichero == false);
                    }
                } while (dEncontrados[0].ToString() == "");

            }

        }//Listo

        /// <CreaNuevosDirectoriosInfo>
        /// Crea los nuevos Subdirectorios en la ruta nueva indicada con el mismo nombre que los subdirectorios de la ruta Origen
        /// </CreaNuevosDirectoriosInfo>
        /// <param name="rutaDestino">Ruta origen de donde leera los subdirectorios que contenga</param>
        public static string[] CreaNuevosDirectorios()
        {           
            for (int i = 0; i < directorios.Length; i++)
            {
                string apoyoruta = rutaDestino;//Variable para conservar la ruta original
                apoyoruta += Path.DirectorySeparatorChar.ToString() + directorios[i].ToString();
                Directory.CreateDirectory(apoyoruta);
                arryRutasNuevasTmp[i] = apoyoruta;//Guardamos las nuevas rutas, con los nuevos subdirectorios del nuevo directorio Raiz.
                arryRutasNuevas[i] = arryRutasNuevasTmp[i];//guardo las nuevas rutas en otro Array para intentar sacarlo tambien del Metodo. 
            }
            return arryRutasNuevas;
        }   //Listo

      

        /// <DevuelveRutasOriginalesExeInfo>
        /// Este metodo coge el nombre del ultimo directorio(el cual coincide el nombre del proyecto y el de el ejecutable)
        /// avanza en el nivel directorios por "bin" y "Debug" y dentro de esta ultima busca el ejecutable que coincida con el mismo nombre que el proyecto
        /// y por ultimo lo guarda en la coleccion.
        /// </DevuelveRutasOriginalesExeInfo>
        /// <returns>coleccion de rutas originales hasta los ejecutables .exe</returns>
        public static string[] DevuelveRutasOriginalesExe()
        {
            string[] partesDeUnaRutaOriginal;
            string nombreejecutable = string.Empty;
            Arraynombreejecutables = new string[ArrayRutasOriginales.Length];
           
            for (int i = 0; i < ArrayRutasOriginales.Length; i++)
            {
                partesDeUnaRutaOriginal = ArrayRutasOriginales[i].Split(Path.DirectorySeparatorChar);
                nombreejecutable = partesDeUnaRutaOriginal[partesDeUnaRutaOriginal.Length - 1];//Cogemos el ultimo de el array que sera el directorio con el nombre del proyecto (el mismo que del ejecutable final)
                Arraynombreejecutables[i] = partesDeUnaRutaOriginal[partesDeUnaRutaOriginal.Length - 1];
                ArrayRutasOriginalesExe[i] = ArrayRutasOriginales[i] + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug" ;
              
            }
            return ArrayRutasOriginalesExe;
        }//Pendiente de refactorizacion

        public string[] DevuelveNombreDirectorioRaizdeProyecto()
        {
            ArrayComprobacionRutasOriginales = null;
            string[] partesDeUnaRutaOriginal;
            string nombreDirectorioRaizProyecto = string.Empty;
            ArrayNomDirectorioRaizProyecto = new string[ArrayComprobacionRutasOriginales.Length];

            for (int i = 0; i < ArrayComprobacionRutasOriginales.Length; i++)
            {
                partesDeUnaRutaOriginal = ArrayComprobacionRutasOriginales[i].Split(Path.DirectorySeparatorChar);
                nombreDirectorioRaizProyecto = partesDeUnaRutaOriginal[partesDeUnaRutaOriginal.Length - 1];//Cogemos el ultimo de el array que sera el directorio con el nombre del proyecto (el mismo que del ejecutable final)
                ArrayNomDirectorioRaizProyecto[i] = partesDeUnaRutaOriginal[partesDeUnaRutaOriginal.Length - 1];
            }

            return ArrayNomDirectorioRaizProyecto;
        }

        /// <Devuelve el nombre de cada ejecutable>
        /// Devuelve los nombres de cada ejecutable que coincide con el nombre del proyecto
        /// </summary>
        /// <returns>Arraynombreejecutables</returns>
        public static string[] DevuelveNombreProyectos()//Pendiente de refactorizacion
        {
            return Arraynombreejecutables;
        }
    }
}
