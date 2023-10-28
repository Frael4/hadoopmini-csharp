using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2hadoopmini.models
{
    public class Tarea
    {
        Stopwatch stopwatch = new Stopwatch(); //Para medir tiempo

        private string? fileInput;
        private string? fileOuput;
        private int node;

        private MyMap? functionMap;
        private MyReduce? functionReduce;
        private Object? functionJoin;

        public string FileInput { get => fileInput; set => fileInput = value; }
        public string FileOuput { get => fileOuput; set => fileOuput = value; }
        public int Node { get => node; set => node = value; }
        public MyMap FunctionMap { get => functionMap; set => functionMap = value; }
        public MyReduce FunctionReduce { get => functionReduce; set => functionReduce = value; }
        public object FunctionJoin { get => functionJoin; set => functionJoin = value; }

        public override int GetHashCode()
        {
            return HashCode.Combine(fileInput, fileOuput, node, functionMap, functionReduce, functionJoin);
        }

        /// <summary>
        /// Ejecucion de la Tarea/Proceso Map Reduce
        /// </summary>
        public void Run()
        {
            FileHandler fileHandler = new FileHandler(this.fileInput, this.fileOuput);
            BufferMap bfrMap = new BufferMap();
            List<Tupla> result = new List<Tupla>();

            List<Tupla> bufferList = fileHandler.GenerateBufferMap(this.node);

            if (bufferList.Count == 0 )
            {
                Console.WriteLine("No se ha podido cargar el archivo");
                return;
            }

            Console.WriteLine("Iniciando proceso de Map");
            stopwatch.Start();

            foreach(Tupla tupla in bufferList)
            {
                List<Tupla> output = new List<Tupla>();
                functionMap?.map(tupla, output);
                bfrMap.SplitBuffer(output, this.node);
            }

            stopwatch.Stop();
            double duration = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine("El proceso de Map tomo: " + duration + " segundos.");

            Console.WriteLine("Iniciando proceso de Ordenamiento");
            stopwatch.Restart();

            bfrMap.SortBuffer();
            List<BufferReducer> sortedList = bfrMap.GetSortedList();
            stopwatch.Stop();
            duration = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine("El proceso de Ordenamiento tomó: " + duration + " segundos.");

            Console.WriteLine("Iniciando proceso de Reduce");
            stopwatch.Restart();

            foreach (BufferReducer bfrReducer in sortedList)
            {
                List<Tupla> reduceTupleList = bfrReducer.GetListTuples();

                foreach(Tupla reducerTuple in reduceTupleList)
                {
                    functionReduce?.reduce(reducerTuple, result);
                }
            }

            stopwatch.Stop();
            Console.WriteLine("El proceso de Reduce tomó: " + duration + " segundos.");

            Console.WriteLine("Gauardando los datos en: " + fileOuput);
            fileHandler.SaveResults(result);
        }



    }
}
