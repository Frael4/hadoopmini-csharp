using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2hadoopmini.models
{
    public class BufferReducer
    {

        private int numReducer;
        private List<Tupla> listTuples;

        public BufferReducer(int numReducer, List<Tupla> listTuples)
        {
            this.numReducer = numReducer;
            this.listTuples = listTuples;
        }

        public void AddTupleToListOfTuples(Tupla tuple)
        {

            int index = SearchTupleInListOfTuples(tuple);
            List<Object> lastTmp;


            if (index != -1)
            {
                Tupla tmp = this.listTuples.ElementAt(index);
                lastTmp = (List<Object>)tmp.Key;
                lastTmp.Add(tuple.Value);
                this.listTuples.Insert(index, new Tupla { Key = tuple.Key, Value = lastTmp });
            }
            else
            {
                lastTmp = new List<Object>();
                lastTmp.Add((Object)tuple.Value);
                this.listTuples.Add(new Tupla { Key = tuple.Key, Value = lastTmp });
            }

        }

        private int SearchTupleInListOfTuples(Tupla tuple)
        {

            foreach (Tupla t in listTuples)
            {
                int i = listTuples.IndexOf(t);
                string keyTupleTmp = (string)this.listTuples.ElementAt(i).Key;

                if (keyTupleTmp.CompareTo((string)tuple.Key) == 0)
                {
                    return i;
                }

            }

            return -1;
        }

        public int GetNumReducer()
        {
            return this.numReducer;
        }

        public List<Tupla> GetListTuples()
        {
            return this.listTuples;
        }
    }
}
