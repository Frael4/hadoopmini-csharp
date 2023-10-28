using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2hadoopmini.models
{
    public class FileHandler
    {
        private string pathSource;
        private string pathOutput;
        public FileHandler(string fileInput, string fileOuput) {
            this.pathSource = Path.GetFullPath(@"\g2hadoopmini.data\" + fileInput);
            this.pathOutput = @"..\g2hadoopmini.data\resultados\" + fileOuput;
        }

        /// <summary>
        /// Lee el archivo de entrada
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public List<Tupla> GenerateBufferMap(int nodes)
        {
            List<Tupla> tuplesList = new List<Tupla>();

            StringBuilder stringBuilder= new StringBuilder();

            try
            {

                Console.WriteLine("Buscando archivo. ...");

                    Console.WriteLine("Archivo encontrado " + Path.GetFullPath(Directory.GetCurrentDirectory()));
                    Console.WriteLine("Archivo encontrado " + ( this.pathSource));
                if (File.Exists(this.pathSource))
                {

                    using (StreamReader input = new StreamReader(File.OpenRead(this.pathSource)))
                    {
                        string line;
                        while((line = input.ReadLine()) != null)
                        {
                            stringBuilder.Append(line.Trim()).Append(" ");
                        }

                        int hash = stringBuilder.GetHashCode() % nodes;
                        tuplesList.Add(new Tupla { Key = hash, Value = stringBuilder.ToString() });
                    }
                }
                else
                {
                    Console.WriteLine("Archivo no existe");
                }


            }
            catch(IOException e)
            {
                Console.WriteLine("Error en: " + e.Message);
            }

            return tuplesList;
        }

        /// <summary>
        /// Guarda los resultados de la Tarea
        /// </summary>
        /// <param name="result"></param>
        public void SaveResults(List<Tupla> result)
        {
            try
            {

                if(File.Exists(this.pathOutput)) { File.Delete(this.pathOutput);}

                StreamWriter writer = new (this.pathOutput);

                result.ForEach(t =>
                {
                    writer.Write(t.Key + " " + t.Value + "\n");
                });
                writer.Close();
                Console.WriteLine("Resultados creados correctamente");
            }
            catch(IOException e)
            {
                Console.WriteLine("Error en crear archivo de resultados: " + e.Message);
            }

        }
    }
}
