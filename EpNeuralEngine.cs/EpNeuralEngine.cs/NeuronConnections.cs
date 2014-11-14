using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpNeuralEngine.cs
{
    public class NeuronConnections : Dictionary<INeuron, Single>
    {

        public new INeuron Add(INeuron input, Single weight)
        {
            base.Add(input,weight);
            return input;
        }

        public INeuron Add(INeuron input)
        {
            base.Add(input, Util.RandomFloat());
            return input;
        }

        public new void Remove(INeuron obj)
        {
            base.Remove(obj);
        }

        public KeyCollection Neurons()
        {
            return base.Keys;
        }

        public new Single this[INeuron obj]
        {
            get
            {
                return base[obj];
            }
            set
            {
                base[obj] = value;
            }
        }
    }
}
