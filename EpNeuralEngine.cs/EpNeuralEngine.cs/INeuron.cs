using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpNeuralEngine.cs
{
    public static class Global{
        public const Single LEARNING_RATE = 0.5f;
    }
    
    public interface INeuron
    {


        Single BiasValue { get; set; }
        Single OutputValue { get; set; }
        Single DeltaValue { get; set; }
        NeuronCollection ForwardConnections { get; }
        NeuronConnections Inputs { get; }
        INeuronStrategy Strategy { get; set; }
        void UpdateOutput();
        void UpdateDelta(Single errorFactor);
        void UpdateFreeParams();
    }
}
