using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpNeuralEngine.cs
{
    public class BackPropNetworkFactory:INetworkFactory
    {
        public INeuralNetwork CreateNetwork(List<int> neuronsInLayers)
        {
            NeuralNetwork bnn = new NeuralNetwork();
            BackPropNeuronStrategy strategy = new BackPropNeuronStrategy();

            foreach(int neurons in neuronsInLayers)
            {
                NeuronLayer layer=new NeuronLayer();
                for(int i=0;i<neurons;i++)
                {
                    layer.Add(new Neuron(strategy));
                }
                bnn.Layers.Add(layer);
            }
            bnn.ConnectLayers();
            return bnn;
        }

        public INeuralNetwork CreateNetwork(int inputNeurons, int outputNeurons)
        {
            List<int> arr = new List<int>();
            arr.Add(inputNeurons);
            arr.Add(inputNeurons);
            arr.Add(outputNeurons);
            return CreateNetwork(arr);
        }
    }
}
