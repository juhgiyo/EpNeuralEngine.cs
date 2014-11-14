using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpNeuralEngine.cs
{
    public interface INeuralNetwork
    {
        void TrainNetwork(TrainingData data);
        void ConnectNeurons(INeuron source, INeuron destination, Single weight);
        void ConnectNeurons(INeuron source, INeuron destination);
        void ConnectLayers(NeuronLayer layer1, NeuronLayer layer2);
        void ConnectLayers();
        List<object> RunNetwork(List<object> inputs);
        List<object> GetOutput();
        
        NeuronLayerCollection Layers{get; }
        NeuronLayer InputLayer{get; }
        NeuronLayer OutputLayer{get;}

    }
}
