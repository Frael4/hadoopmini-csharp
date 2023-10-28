using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2hadoopmini.models
{
    public class BufferMap
    {
        private readonly List<Tupla> splittedList = new List<Tupla>();
        private readonly List<BufferReducer> sortedList = new List<BufferReducer>();

        public void SplitBuffer(List<Tupla> listTuple, int nodes)
        {
            listTuple.ForEach(t =>
            {
                int nodeReducer = t.Key.GetHashCode() % nodes;
                splittedList.Add(new Tupla { Key = nodeReducer, Value = t });
            });
        }

        public void SortBuffer()
        {

            splittedList.ForEach(t =>
            {
                int key_ = (int)t.Key;
                int position = SearchBufferReducer(key_);
                Tupla tupleOfTuple = (Tupla)t.Value;

                if (position != -1) {
                    BufferReducer bfr = sortedList.ElementAt(position);
                    bfr.AddTupleToListOfTuples(tupleOfTuple);
                    sortedList.Insert(position, bfr);
                }
                else
                {
                    List<Object> tmpListValues = new List<Object>();
                    tmpListValues.Add(tupleOfTuple.Value);

                    List<Tupla> tmp =  new List<Tupla>();
                    tmp.Add(new Tupla { Key = tupleOfTuple.Key, Value = tmpListValues });
                    sortedList.Add(new BufferReducer(key_, tmp));
                }

            });
        }

        private int SearchBufferReducer(int reducer)
        {
            for (int i = 0; i < this.sortedList.Count; i++)
            {
                BufferReducer bfrReducer = this.sortedList.ElementAt(i);
                if(bfrReducer.GetNumReducer() == reducer)
                {
                    return i;
                }
            }
            return -1;
        }

        public List<Tupla> GetSplittedList()
        {
            return this.splittedList;
        }

        public List<BufferReducer> GetSortedList()
        {
            return this.sortedList;
        }
    }
}
