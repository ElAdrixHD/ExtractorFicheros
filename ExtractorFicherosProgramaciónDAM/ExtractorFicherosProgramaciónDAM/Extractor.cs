using System;
using System.IO;

namespace ExtractorFicherosProgramaciónDAM
{
    public class Extractor
    {
        static int contadorRecursivo = 0;
        static string[][] lista;
        static int contador = 0;
        static string[] carpetasDePegado;

        /// <summary>
        /// Lee los directorios de la primera carpeta (el cual contiene el numero de ejercicios) y los guarda en un array dentado.
        /// </summary>
        /// <param name="rutaInicio">Parametro de ruta de origen</param>
        /// <param name="rutaDestino">Parametro de ruta destino</param>
        /// <returns>Devuelve un array de strings dentado los cuales contiene los ficheros fuentes, los ejecutables y dlls.</returns>
        public static string[][] BusquedaPrimera(string rutaInicio, string rutaDestino)
        {
            lista = new string[Directory.GetDirectories(rutaInicio).Length][];
            try
            {
                foreach (string directorios in Directory.GetDirectories(rutaInicio))
                {
                    lista[contador] = new string[10];
                    BusquedaRecursiva(directorios);
                    contador++;
                    contadorRecursivo = 0;
                }
                carpetasDePegado = Directory.GetDirectories(rutaInicio);
                CrearCarpetas(rutaDestino, carpetasDePegado);
                return lista;
            }
            catch (Exception)
            {
                return null;
            } 
        }

        public static void BusquedaRecursiva(string ruta)
        {
            try
            {
                foreach (string directorios in Directory.GetDirectories(ruta))
                {
                    foreach (string ficheros in Directory.GetFiles(directorios))
                    {
                        if ((Path.GetExtension(ficheros) == ".cs" || (Path.GetExtension(ficheros) == ".exe" && !Path.GetFileName(ficheros).Contains(".vshost.exe")) || Path.GetExtension(ficheros) == ".dll") && (!Path.GetFullPath(ficheros).Contains("obj") && !Path.GetFullPath(ficheros).Contains("Properties")))
                        {
                            lista[contador][contadorRecursivo++] = ficheros;
                        }
                    }
                    BusquedaRecursiva(directorios);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void CrearCarpetas(string rutaDestino, string[] carpetas)
        {
            for (int i = 0; i < lista.Length; i++)
            {
                Directory.CreateDirectory(rutaDestino + "\\" + new DirectoryInfo(carpetasDePegado[i]).Name);
            }
        }

        public static bool Copiar(string[][] lista, string ruta)
        {
            int numero = 0;
            for (int i = 0; i < lista.Length; i++)
            {
                numero++;
                for (int j = 0; j < lista[i].Length; j++)
                {
                    try
                    {
                        if (lista[i][j] != null)
                        {
                            File.Copy(lista[i][j], ruta + "\\" + new DirectoryInfo(carpetasDePegado[i]).Name + "\\" + Path.GetFileName(lista[i][j]));
                        }     
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void EliminarCarpetasVacias(string rutaDestino)
        {
            foreach (string directorios in Directory.GetDirectories(rutaDestino))
            {
                if (Directory.GetFiles(directorios).Length == 0 && Directory.GetDirectories(directorios).Length == 0)
                {
                    Directory.Delete(directorios);
                }
            }
        }
    }
}
