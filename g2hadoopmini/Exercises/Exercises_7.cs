using g2hadoopmini.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2hadoopmini.Exercises
{
    public class Exercises_7
    {
        internal class MyMapImplementation : MyMap
        {
            public void map(Tupla element, List<Tupla> output)
            {

                string[] line = element.Value.ToString().Split(' ');
                foreach (string item in line)
                {
                    string[] lineData = item.Split('\t');
                    if (lineData.Length > 2 && double.TryParse(lineData[2], out double happinessAverage))
                    {
                        if (happinessAverage < 2 && lineData[4] != "--")
                        {
                            output.Add(new Tupla() { Key = "Palabras extremadamente tristes: ", Value = 1 });
                        }
                    }
                }
            }
        }

        internal class MyReduceImplementation : MyReduce
        {
            public void reduce(Tupla element, List<Tupla> output)
            {
                List<Int64> integerslist = (List < Int64 >) element.Key;
                int count = 0;
                foreach(int item in integerslist){
                    count += item;
                }
                output.Add(new Tupla() { Key = element.Key, Value = count });
            }
        }

        public static void Run()
        {
            Tarea tarea = new();
            tarea.FileInput = "happiness.txt";
            tarea.FileOuput = "Results_7.txt";
            Console.WriteLine("Ingrese el numero de nodos");
            int nodes = int.Parse(Console.ReadLine());
            tarea.Node = nodes;
            tarea.FunctionMap = new MyMapImplementation();
            tarea.FunctionReduce = new MyReduceImplementation();
            tarea.Run();

        }
    }
}
