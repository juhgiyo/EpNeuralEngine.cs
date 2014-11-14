using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpNeuralEngine.cs
{
    public interface INeuronStrategy
    {
        Single FindDelta(Single output, Single errorFactor);
        Single Activation(Single value);
        Single FindNetValue(NeuronConnections inputs, Single bias);
        Single FindNewBias(Single bias, Single delta);
        void UpdateWeights(NeuronConnections connections, Single delta);
    }
}
