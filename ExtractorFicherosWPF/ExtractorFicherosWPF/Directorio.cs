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
        string rutaDirectoriosNuevos = string.Empty;//Directorios nuevos donde copiaremos los ficheros. 
        string rutaParaSubdirectorio = string.Empty;//Ruta hacia la carpeta raiz original donde leeremos todos los subdirectorios originales.
        string[] arryRutasNuevas;//Colecion de rutas nuevas des de el directorio raiz nuevo hasta cada uno de los subdirectorios       
        bool hayfichero = false;//Comprueba si encontro un fichero o n o en el nivel de directorios que se encuentra.
        string[] ArrayRutasOriginales;//Rutas originales hasta el directorio mas proximo a los ".cs"
        string[] Arraynombreejecutables;//Array de cada uno de los nombres de los ejecutables a buscar"ya que podemos encontrar varios .exe en un proyecto".
        string[] ArrayRutasOriginalesExe;//Array de rutas hasta los ejecutables .exe
        static string rutaOrigen = string.Empty;
        static string rutaDestino = string.Empty;

        #endregion 
        //---------------------------------------------------------------------------------------------------------------------
        public string AbrirDialogo()
        {
            string path = string.Empty;
            FolderBrowserDialog dialogoDirectorio = new FolderBrowserDialog();
            DialogResult resultado;

            try
            {
                dialogoDirectorio.ShowNewFolderButton = true;
                resultado = dialogoDirectorio.ShowDialog();
                path = dialogoDirectorio.SelectedPath;
                rutaParaSubdirectorio = path;
                dialogoDirectorio.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
            return path;
        }

        public string CargarRutaOrigen()
        {
            return rutaOrigen = AbrirDialogo();
        }

        public string CargarRutaDestino()
        {
            return rutaDestino = AbrirDialogo();
        }
        //-----------------------------------------------------------------------------------------------------------------------


        /// <Metodo que Lee el Directorio raiz>
        /// Lee el directorio raiz, y monta las rutas hasta los ficheros ".cs"
        /// </summary>
        /// <returns>ArrayRutasOriginales</returns>
        public string[] LecturaSubDirectorios()
        {
            string subdirectorioanadidofinal = string.Empty;//Variable usada para añadir el ultimo subdirectorio mas profundo antes de llegar a los ficheros     
            string rutatmp = string.Empty; //ruta temporal , para montar la ruta hacia los nuevos directorios que se van a crear iguales que los originale, excepto el raiz.

            DirectoryInfo d = new DirectoryInfo(rutaOrigen);
            DirectoryInfo[] directorios = d.GetDirectories();
            ArrayRutasOriginales = new string[directorios.Length];
            ArrayRutasOriginalesExe = new string[ArrayRutasOriginales.Length];
            arryRutasNuevas = new string[directorios.Length];
            string[] arryRutasNuevasTmp = new string[ArrayRutasOriginales.Length];//Array de rutas donde se han creado cada uno de los nuevos subdirectorios en el nuevo directorio Raiz.  
            Arraynombreejecutables = new string[ArrayRutasOriginales.Length];

            for (int i = 0; i < directorios.Length; i++)
            {
                ArrayRutasOriginales[i] = rutaOrigen + "\\" + directorios[i];//Monta y guarda todas las rutas des de la Raiz hasta cada uno de los subdirectorios.

            }

            #region Creacion de directorios

            //Crea los nuevos Subdirectorios...(No se realizo en su metodo, ya que necesitamos  la longitud del array declarado en este."directorios""
            rutatmp = rutaDestino;
            for (int i = 0; i < directorios.Length; i++)
            {
                string apoyoruta = rutatmp;//Variable para conservar la ruta original
                apoyoruta += Path.DirectorySeparatorChar.ToString() + directorios[i].ToString();
                Directory.CreateDirectory(apoyoruta);
                arryRutasNuevasTmp[i] = apoyoruta;//Guardamos las nuevas rutas, con los nuevos subdirectorios del nuevo directorio Raiz.
                arryRutasNuevas[i] = arryRutasNuevasTmp[i];//guardo las nuevas rutas en otro Array para intentar sacarlo tambien del Metodo.           
            }

            #endregion

            #region Busqueda mas profunda

            //POSIBLE METODO LLAMADO "BUSQUEDA PROFUNDA"
            //-Remontando rutas originales finales "relacionejercicios\ejercicio1\app_reloj"

            for (int i = 0; i < ArrayRutasOriginales.Length; i++)
            {
                string tmp = string.Empty;

                DirectoryInfo directorio2 = new DirectoryInfo(ArrayRutasOriginales[i]);
                DirectoryInfo[] dEncontrados = directorio2.GetDirectories();
                do
                {
                    if (dEncontrados[0].ToString() != "")
                    {
                        //Este buble comprueba que en el nivel de la ruta en el que se encuentra hay un fichero ".cs " si no es asi busca otro subdirectorio y avanza hasta encontrarlos.

                        do
                        {
                            subdirectorioanadidofinal = dEncontrados[0].ToString();
                            tmp = ArrayRutasOriginales[i];
                            tmp += Path.DirectorySeparatorChar.ToString() + subdirectorioanadidofinal;
                            ArrayRutasOriginales[i] = tmp;
                            Fichero mifichero = new Fichero();
                            hayfichero = mifichero.CompruebaFichero(ArrayRutasOriginales[i]);
                        } while (hayfichero == false);
                        

                    }
                } while (dEncontrados[0].ToString() == "");

            }


            #endregion
            return ArrayRutasOriginales;
        }

        /// <Metodo que crea la ruta hacia el nuevo directorio raiz>
        /// Este metodo crea  la ruta hacia el nuevo directorio raiz donde se crearan todos
        /// los de mas subdirectorios 
        /// (con el nombre identico de cada uno de los subdirectorios de la carpeta raiz original)
        /// </summary>
        /// <returns>RutaDirectoriosNuevos</returns>
        /*public string CreaDirectorios()
        {

            string datosRutaSubdirectNuevos = string.Empty;
            string nombreDirectorioNuevo = string.Empty;

            Console.WriteLine("\n-Dime la letra del volumen donde voy a crear el nuevo directorio con el contenido copiado.");
   
            datosRutaSubdirectNuevos = Console.ReadLine();
            rutaDirectoriosNuevos = datosRutaSubdirectNuevos + Path.VolumeSeparatorChar.ToString();
   
            Console.WriteLine("\n-Dime el nombre del nuevo directorio raiz a crear.\n\"Los Subdirectorios se llamaran igual que los de el directorio origen.\"");
          
            nombreDirectorioNuevo = Console.ReadLine();
            rutaDirectoriosNuevos += Path.DirectorySeparatorChar.ToString() + nombreDirectorioNuevo;
            Directory.CreateDirectory(rutaDirectoriosNuevos);

            Console.WriteLine("\n---------------Directorio \"" + nombreDirectorioNuevo + "\" creado!.-------------------\n");

            return rutaDirectoriosNuevos;
        }*/

        /// <Metodo que deuvle las nuevas rutas creadas>
        /// Devuelve todas y cada una de las subrutas nuevas creadas al crear un nuevo directorio raiz
        /// donde copiaremos todos los proyectos en sus respectivas carpetas
        /// </summary>
        /// <returns>arrayRutasNuevas</returns>
        public string[] DevuelveRutasNuevas()
        {
            return arryRutasNuevas;
        }

        /// <Devuelve las rutas originales hasta los ejecutables encontrados>
        /// Este metodo coge el nombre del ultimo directorio(el cual coincide el nombre del proyecto y el de el ejecutable)
        /// avanza en el nivel directorios por "bin" y "Debug" y dentro de esta ultima busca el ejecutable que coincida con el mismo nombre que el proyecto
        /// y por ultimo lo guarda en la coleccion o array.
        /// </summary>
        /// <returns>ArrayRutasOriginalesExe</returns>
        public string[] DevuelveRutasOriginalesExe()
        {
            string[] partesDeUnaRutaOriginal;
            string nombreejecutable = string.Empty;
            Arraynombreejecutables = new string[ArrayRutasOriginales.Length];
            for (int i = 0; i < ArrayRutasOriginales.Length; i++)
            {
                partesDeUnaRutaOriginal = ArrayRutasOriginales[i].Split(Path.DirectorySeparatorChar);
                nombreejecutable = partesDeUnaRutaOriginal[partesDeUnaRutaOriginal.Length - 1];//Cogemos el ultimo de el array que sera el directorio con el nombre del proyecto (el mismo que del ejecutable final)
                Arraynombreejecutables[i] = partesDeUnaRutaOriginal[partesDeUnaRutaOriginal.Length - 1];
                ArrayRutasOriginalesExe[i] = ArrayRutasOriginales[i] + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug" + Path.DirectorySeparatorChar + nombreejecutable + ".exe";
            }
            return ArrayRutasOriginalesExe;
        }
        /// <Devuelve el nombre de cada ejecutable>
        /// Devuelve los nombres de cada ejecutable que coincide con el nombre del proyecto
        /// </summary>
        /// <returns>Arraynombreejecutables</returns>
        public string[] DevuelveNombreProyectos()
        {
            return Arraynombreejecutables;
        }
    }
}
