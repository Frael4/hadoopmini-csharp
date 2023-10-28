using g2hadoopmini.models;

namespace g2hadoopmini.Exercises
{
    public class Exercises_1
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
                
            }
        }

        public static void Run(string[] args)
        {
            Tarea tarea = new();
            tarea.FileInput = "weblog.txt";
            tarea.FileOuput = "Results_1.txt";
            Console.WriteLine("Ingrese el numero de nodos");
            int nodes = int.Parse(Console.ReadLine());
            tarea.Node = nodes;
            tarea.FunctionMap = new MyMapImplementation();


        }
    }
}
