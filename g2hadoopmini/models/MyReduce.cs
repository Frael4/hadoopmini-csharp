
namespace g2hadoopmini.models
{
    public interface MyReduce
    {
        
        /// <summary>
        /// Funcion de reduce
        /// </summary>
        /// <param name="element"></param>
        /// <param name="output"></param>
        void reduce(Tupla element, List<Tupla> output);
    }
}
