namespace g2hadoopmini.models
{
    public interface MyMap
    {
        
        /// <summary>
        /// Funcion mapeadora
        /// </summary>
        /// <param name="element"></param>
        /// <param name="output"></param>
        void map(Tupla element, List<Tupla> output);
    }
}