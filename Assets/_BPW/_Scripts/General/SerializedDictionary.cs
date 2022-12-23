using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class SerializedDictionary<T, P>
{
    [Serializable]
    private struct Pair
    {
        public T Key;
        public P Value;
        public Pair(T key, P value) {
            Key = key;
            Value = value;
        }
    }

    public List<T> Keys { get; private set; } = new List<T>();
    public List<P> Values { get; private set; } = new List<P>();

    public SerializedDictionary(List<T> keys, List<P> values) {
        Keys = keys;
        Values = values;
    }
}