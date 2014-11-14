using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpNeuralEngine.cs
{
    public interface INetworkFactory
    {
        INeuralNetwork CreateNetwork(int inputNeurons, int outpuNeurons);
    }
}
