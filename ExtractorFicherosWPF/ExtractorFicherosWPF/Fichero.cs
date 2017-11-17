using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Añadido
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace ExtractorFicherosWPF
{
    class Fichero
    {

        #region Variables globales   
        Directorio miDirectorio = new Directorio();
        string[] arryRutasOriginales;
        string[] arrayRutasExe;//Array donde guardaremos la coleccion de rutas originales hasta los ejecutables
        string[] arryUnaClase;//Donde guardaremos la ruta hacia cada fichero ".cs" de un proyecto.
        string[] arrayNombreProyectos;
        string[] Rutasnuevas;

        #endregion

        /// <Metodo de Lectura de ficheros Cs>
        /// Lee todos los ficheros  dentro de un directorio concreto y los copia cada uan de las nuevas rutas respectivas
        /// <Metodo de Lectura de ficheros Cs>

        public bool LecturaFicheros()
        {
            try
            {
                arryRutasOriginales = miDirectorio.LecturaSubDirectorios();
                arrayRutasExe = miDirectorio.DevuelveRutasOriginalesExe();
                Rutasnuevas = miDirectorio.DevuelveRutasNuevas();//Guarda las nuevas rutas definidas por el usuario.

                for (int i = 0; i < arryRutasOriginales.Length; i++)
                {

                    DirectoryInfo directorio = new DirectoryInfo(arryRutasOriginales[i]);
                    FileInfo[] fichero;

                    fichero = directorio.GetFiles("*.cs");// Busca los "*.cs" 

                    for (int k = 0; k < fichero.Length; k++)
                    {


                        arryUnaClase = new string[fichero.Length];//Creamos un array con la longitud de la cantiadad de ficheros encontrados
                        Console.WriteLine("[{0}] Nombre fichero -> {1} .", k + 1, fichero[k].Name.ToString());
                        string tmp = string.Empty;
                        tmp = fichero[k].ToString(); ;
                        tmp = fichero[k].FullName.ToString();
                        Console.WriteLine("\n\t\t---Fin Ficheros de esa Ruta ----\n");

                        if (fichero.LongLength != 0)//Si encontro algo
                        {
                            tmp = (fichero[k].FullName).ToString();
                            arryUnaClase[k] = fichero[k].FullName.ToString();
                            string nombreFichero = string.Empty;
                            nombreFichero = fichero[k].Name;


                            //Copia cada fichero ".cs " a su directorio correspondiente
                            FileInfo mifichero2 = new FileInfo(arryUnaClase[k]);
                            mifichero2.CopyTo(Rutasnuevas[i] + Path.DirectorySeparatorChar + nombreFichero);


                        }
                    }

                }
            }
            catch(Exception ex)
            {
               
                 MainWindow.MensajeError(ex);
  
                return false;
                
            }
            return true;

        }

        /// <Lee la ruta hacia los ficheros ejecutables(.exe)>
        /// Lee la ruta hacia los ficheros ejecutables y los copia en cada uan de la nuevas rutas correspondientes.
        /// </summary>
        public void LecturaFicheroExe()
        {
            try
            {
                for (int i = 0; i < arryRutasOriginales.Length; i++)
                {

                    arrayNombreProyectos = miDirectorio.DevuelveNombreProyectos();
                    DirectoryInfo directorio2 = new DirectoryInfo(arryRutasOriginales[i] + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug");
                    FileInfo[] fichero2;
                    fichero2 = directorio2.GetFiles("*"+arrayNombreProyectos[i] + ".exe");// Busca los "*.exe" <-----PRUEBA!

                    foreach (FileInfo fichero in fichero2)
                    {
                        FileInfo mifichero3 = new FileInfo(fichero.FullName);
                        string combinacionRutasFinales = string.Empty;
                        combinacionRutasFinales = Rutasnuevas[i] + Path.DirectorySeparatorChar + arrayNombreProyectos[i] + ".exe";                       
                        File.Copy(mifichero3.ToString(), combinacionRutasFinales);

                    }
                }
            }
            catch(Exception ex) {

                MainWindow.MensajeError(ex);
            }
        }


        public bool CompruebaFichero(string ruta)
        {
            bool hayficheros;
            DirectoryInfo directorio = new DirectoryInfo(ruta);
            if (directorio.GetFiles("*.cs").Length != 0)
            {
                hayficheros = true;//Encontro esos ficheros.
            }
            else
            {
                hayficheros = false;//No encontro esos ficheros.
            }

            return hayficheros;
        }
    }
}
