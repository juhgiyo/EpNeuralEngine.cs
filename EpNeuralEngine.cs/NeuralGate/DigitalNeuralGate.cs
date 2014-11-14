using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpNeuralEngine.cs;

namespace NeuralGate
{
    public class DigitalNeuralGate
    {
        private INeuralNetwork network;

        public DigitalNeuralGate()
        {
            BackPropNetworkFactory factory = new BackPropNetworkFactory();
            List<int> layers=new List<int>();
            layers.Add(2);
            layers.Add(2);
            layers.Add(1);
            network = factory.CreateNetwork(layers);
        }

        public void Train(int input1, int input2, int output)
        {
            TrainingData data = new TrainingData();
            data.Inputs.Add(input1);
            data.Inputs.Add(input2);
            data.Outputs.Add(output);
            network.TrainNetwork(data);
        }

        public double Run(int input1, int input2)
        {
            List<object> inputs=new List<object>();
            inputs.Add(input1);
            inputs.Add(input2);

            List<object> outputs = network.RunNetwork(inputs);
            double retVal = Convert.ToDouble(outputs[0]);
            return retVal;
        }
    }
}
