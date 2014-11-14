using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpNeuralEngine.cs
{
    public class NeuronLayerCollection:List<NeuronLayer>
    {
        public new NeuronLayer Add(NeuronLayer obj)
        {
            base.Add(obj);
            return obj;
        }

        public NeuronLayer Add()
        {
            NeuronLayer layer = new NeuronLayer();
            base.Add(layer);
            return layer;
        }

        public new void Insert(int index, NeuronLayer obj)
        {
            base.Insert(index, obj);
        }

        public new void Remove(NeuronLayer obj)
        {
            base.Remove(obj);
        }

        public new NeuronLayer this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
            }
        }
    }
}
