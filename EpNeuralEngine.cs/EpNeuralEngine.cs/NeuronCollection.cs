using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EpNeuralEngine.cs
{
    public class NeuronCollection : List<INeuron>
    {
        public new INeuron Add(INeuron obj)
        {
            base.Add(obj);
            return obj;
        }

        public INeuron Add()
        {
            INeuron neuron = new Neuron();
            neuron.BiasValue = Util.RandomInt();
            base.Add(neuron);
            return neuron;
        }

        public new void Insert(int index, INeuron obj)
        {
            base.Insert(index, obj);
        }

        public new void Remove(INeuron obj)
        {
            base.Remove(obj);
        }

        public new INeuron this[int index]
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
};
