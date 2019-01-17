using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

/*
 *      I M P O R T A N T E___________________________________________________ G R A B A R    A     F U E G O__________________________________________________________________________
 * 
 * 
 *                          TODOS ESTOS MÉTODOS SE LES CAPTURA TODAS LAS EXCEPCIONES Y ESTAS SE ELEVAN PARA QUE LAS RECIBA EL MAIN WINDOWS
 * 
 * 
 *                          EL CUAL SE ENCARGA DE QUE SI SALTÓ ALGUNA EXCEPCION, SALGA EN MENSAJE DE ERROR.
 * 
 * 
 */



namespace ExtractorFicherosProgramaciónDAM
{
    public class Extractor
    {
        static string[][] ficherosOrdenadosPorEjercicios;
        static string[] carpetasDePegado; //Aqui se guardan el nombre de las carpetas de los ejercicios para que sean copiados con el mismo nombre luego
        static List<string> listaTemporal; //Esta lista se utiliza como lista temporal ya que se usa para una carpeta y se borra luego los datos para la proxima carpeta.

        /// <summary>
        /// Lee los directorios de la primera carpeta (el cual contiene el numero de ejercicios) y los guarda en un array dentado.
        /// </summary>
        /// <param name="rutaInicio">Parametro de ruta de origen</param>
        /// <param name="rutaDestino">Parametro de ruta destino</param>
        /// <returns>Devuelve un array de strings dentado los cuales contiene los ficheros fuentes, los ejecutables y dlls.</returns>
        public static void BusquedaPrimera(string rutaInicio, string rutaDestino, bool _compresion, string nombreComprimido = null)
        {
            ComprobarCarpetaDestino(rutaDestino);
            listaTemporal = new List<string>();
            ficherosOrdenadosPorEjercicios = new string[Directory.GetDirectories(rutaInicio).Length][];
            carpetasDePegado = Directory.GetDirectories(rutaInicio);
            int contador = 0;
            try
            {
                foreach (string directorios in Directory.GetDirectories(rutaInicio))
                {
                    Busqueda(directorios);
                    ficherosOrdenadosPorEjercicios[contador++] = listaTemporal.ToArray();   //Determina de cada fila, cuantos archivos tiene cada carpeta
                    listaTemporal.Clear();
                }
                CrearCarpetas(rutaDestino, carpetasDePegado); //No se puede crear las carpetas hasta que "BusquedaRecursiva haya sido completado y el array dentado esté lleno de datos."
                Copiar(ficherosOrdenadosPorEjercicios, rutaDestino);
                EliminarCarpetasVacias(rutaDestino);
                if (_compresion)
                {
                    Compresion(rutaDestino, nombreComprimido);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Primero comprueba si existe los ficheros fuentes o exes y luego hace las busquedas recursivas.
        /// </summary>
        /// <param name="ruta"></param>
        private static void Busqueda(string ruta)
        {
            foreach (string fichero in Directory.GetFiles(ruta))
            {
                BuscarArchivos(fichero);
            }
            BusquedaRecursiva(ruta);
        }

        /// <summary>
        /// Dado la carpeta de cada ejercicio, de forma recursiva, busca si hay ficheros fuentes, ejecutables o dll.
        /// </summary>
        /// <param name="ruta">Carpeta de uno de los ejercicios.</param>
        private static void BusquedaRecursiva(string ruta)
        {
            try
            {
                foreach (string directorios in Directory.GetDirectories(ruta))
                {
                    foreach (string ficheros in Directory.GetFiles(directorios))
                    {
                        BuscarArchivos(ficheros);
                    }
                    BusquedaRecursiva(directorios);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void BuscarArchivos(string ficheros)
        {
            if ((Path.GetExtension(ficheros) == ".cs" || (Path.GetExtension(ficheros) == ".exe" && !Path.GetFileName(ficheros).Contains(".vshost.exe")) || Path.GetExtension(ficheros) == ".dll") && (!Path.GetFullPath(ficheros).Contains("obj") && !Path.GetFullPath(ficheros).Contains("Properties")))
            {
                listaTemporal.Add(ficheros); //Se añade a la lista temporal para luego ser procesado.
            }
        }

        /// <summary>
        /// Crea las carpetas dado un array con las ruta de los ejercicios.
        /// </summary>
        /// <param name="rutaDestino">Ruta de creación de las carpetas</param>
        /// <param name="carpetas">Array de las carpetas</param>
        private static void CrearCarpetas(string rutaDestino, string[] carpetas)
        {
            try
            {
                for (int i = 0; i < ficherosOrdenadosPorEjercicios.Length; i++)
                {
                    Directory.CreateDirectory(rutaDestino + "\\" + new DirectoryInfo(carpetasDePegado[i]).Name); //DirectoryInfo saca el nombre de cada una de las carpetas
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Copia los archivos encontrados y guardados en la matriz en la ruta de destino seleccionada.
        /// </summary>
        /// <param name="lista">Array con los ficheros de cada ejercicio</param>
        /// <param name="ruta">Ruta de destino</param>
        /// <returns>Verdadero o Falso</returns>
        private static void Copiar(string[][] lista, string ruta)
        {
            try
            {
                int numero = 0;
                for (int i = 0; i < lista.Length; i++)
                {
                    numero++;
                    for (int j = 0; j < lista[i].Length; j++)
                    {
                        File.Copy(lista[i][j], ruta + "\\" + new DirectoryInfo(carpetasDePegado[i]).Name + "\\" + Path.GetFileName(lista[i][j]));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Despues de haber copiado los ficheros en cada carpeta, comprobamos si dichas carpetas estan vacias, en caso afirmativo, se procede a su destrucción.
        /// </summary>
        /// <param name="rutaDestino">Reuta de destino</param>
        private static void EliminarCarpetasVacias(string rutaDestino)
        {
            try
            {
                foreach (string directorios in Directory.GetDirectories(rutaDestino))
                {
                    if (Directory.GetFiles(directorios).Length == 0 && Directory.GetDirectories(directorios).Length == 0)
                    {
                        Directory.Delete(directorios, true);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private static void Compresion(string ruta, string nombre)
        {
            try
            {
                string rutaDirectorioPadre = new DirectoryInfo(ruta).Parent.FullName + "\\";
                ZipFile.CreateFromDirectory(ruta, rutaDirectorioPadre + nombre + ".zip");
                Directory.Delete(ruta,true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void ComprobarCarpetaDestino(string rutaDestino)
        {
            try
            {
                if(new DirectoryInfo(rutaDestino).GetDirectories().Length > 0 || new DirectoryInfo(rutaDestino).GetFiles().Length > 0)
                {
                    throw new Exception("La carpeta de destino no está vacia.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
