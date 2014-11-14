using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpNeuralEngine.cs
{
    public class NeuralNetworkException : NeuralEngineException
    {
        public NeuralNetworkException(string message, Exception e)
            : base(message, e)
        {
        }
    }

    public class NotEnoughLayersException : NeuralEngineException
    {
        public NotEnoughLayersException(string message, Exception e)
            : base(message, e)
        {
        }
    }

    public class TrainingException : NeuralEngineException
    {
        public TrainingException(string message, Exception e)
            : base(message, e)
        {
        }
    }

    public class InvalidInputException : NeuralEngineException
    {
        public InvalidInputException(string message, Exception e)
            : base(message, e)
        {
        }
    }

    public class InvalidOutputException : NeuralEngineException
    {
        public InvalidOutputException(string message, Exception e)
            : base(message, e)
        {
        }
    }

    public class RunningException : NeuralEngineException
    {
        public RunningException(string message, Exception e)
            : base(message, e)
        {
        }
    }

    public class NeuralNetwork : INeuralNetwork
    {
        private NeuronLayerCollection layers = new NeuronLayerCollection();

        public void TrainNetwork(TrainingData data)
        {
            if (layers.Count < 2)
                throw new NotEnoughLayersException("You should have at least two layers in your neural network to train it", null);
            if (data.Inputs.Count != InputLayer.Count)
                throw new InvalidInputException("Number of inputs doesn't match number of neurons in input layer", null);
            if (data.Outputs.Count != OutputLayer.Count)
                throw new InvalidOutputException("Number of outputs doesn't match number of neurons in output layer", null);



            for (int counter = 0; counter < data.Inputs.Count; counter++)
            {
                try
                {
                    data.Inputs[counter] = (object)Convert.ToSingle(data.Inputs[counter]);
                }
                catch (System.Exception ex)
                {
                    throw new InvalidInputException("Unable to convert the input value at location " + (counter + 1) + " to Single",ex);
                }
            }
            for (int counter = 0; counter < data.Outputs.Count; counter++)
            {
                try
                {
                    data.Outputs[counter] = (object)Convert.ToSingle(data.Outputs[counter]);
                }
                catch (System.Exception)
                {
                    throw new InvalidInputException("Unable to convert the output value at location " + (counter + 1) + " to Single", null);
                }
            }

            try
            {
                int i = 0;
                foreach(var someNeuron in InputLayer)
                {
                    someNeuron.OutputValue = Convert.ToSingle(data.Inputs[i]);
                    i += 1;
                }
                for (int count = 1; count < layers.Count; count++)
                {
                    NeuronLayer nl = layers[count];
                    foreach (var someNeuron in nl)
                    {
                        someNeuron.UpdateOutput();
                    }
                }

                i = 0;
                foreach (var someNeuron in OutputLayer)
                {
                    someNeuron.UpdateDelta((float)data.Outputs[i] - someNeuron.OutputValue);
                    i += 1;
                }

                for (i = layers.Count - 2; i >= 1; i--)
                {
                    NeuronLayer currentLayer = layers[i];
                    foreach (var someNeuron in currentLayer)
                    {
                        Single errorFactor = 0.0f;
                        foreach (var connectedNeuron in someNeuron.ForwardConnections)
                        {
                            errorFactor = errorFactor + (connectedNeuron.DeltaValue * connectedNeuron.Inputs[someNeuron]);
                        }
                        someNeuron.UpdateDelta(errorFactor);
                    }
                }

                for (i = 1; i < layers.Count;i++ )
                {
                    foreach (var someNeuron in layers[i])
                    {
                        someNeuron.UpdateFreeParams();
                    }
                }

            }
            catch (System.Exception ex)
            {
                throw new TrainingException("Error occurred while training network. ", ex);
            }

        }

        public NeuronLayerCollection Layers { 
            get
            {
                return layers;
            } 
        }
        public NeuronLayer InputLayer { 
            get 
            {
                if (layers.Count < 2)
                    throw new NotEnoughLayersException("You should have at least two layers in your neural network to train it", null);
                    return layers[0];
            }
        }
        public NeuronLayer OutputLayer
        {
            get
            {
                if (layers.Count < 2)
                    throw new NotEnoughLayersException("You should have at least two layers in your neural network to train it", null);
                return layers[layers.Count - 1];
            }
        }

        public List<object> GetOutput()
        {
            List<object> arr = new List<object>();
            foreach (var someNeuron in OutputLayer)
            {
                arr.Add(someNeuron.OutputValue);
            }
            return arr;
        }

        public void ConnectNeurons(INeuron source, INeuron destination, Single weight)
        {
            if (layers.Count < 2)
                throw new NotEnoughLayersException("You should have at least two layers in your neural network to train it", null);
            destination.Inputs.Add(source, weight);
            source.ForwardConnections.Add(destination);
        }

        public void ConnectNeurons(INeuron source, INeuron destination)
        {
            if (layers.Count < 2)
                throw new NotEnoughLayersException("You should have at least two layers in your neural network to train it", null);
            ConnectNeurons(source, destination, Util.RandomSingle());
        }
        public void ConnectLayers(NeuronLayer layer1, NeuronLayer layer2)
        {
            if (layers.Count < 2)
                throw new NotEnoughLayersException("You should have at least two layers in your neural network to train it", null);

            foreach (var inputNeuron in layer1)
            {
                foreach (var targetNeuron in layer2)
                {
                    ConnectNeurons(inputNeuron, targetNeuron);
                }
            }
        }
        public void ConnectLayers()
        {
            try
            {
                for (int i = 1; i < layers.Count; i++)
                {
                    ConnectLayers(layers[i - 1], layers[i]);
                }
            }
            catch (System.Exception ex)
            {
                throw new NeuralEngineException("Error occurred while trying to connect neuron layers. See stack trace for details", ex);
            }
            
        }

        public List<object> RunNetwork(List<object> inputs)
        {
            if (inputs.Count != InputLayer.Count)
                throw new InvalidInputException("Number of inputs doesn't match number of neurons in input layer", null);

            for (int counter = 0; counter < inputs.Count; counter++)
            {
                try
                {
                    inputs[counter] =(object)Convert.ToSingle(inputs[counter]);
                }
                catch (System.Exception)
                {
                    throw new InvalidInputException("Unable to convert the  input value at location " + (counter + 1) + " to Single", null);
                }
            }

            try
            {
                int i = 0;
                foreach (var someNeuron in InputLayer)
                {
                    someNeuron.OutputValue = Convert.ToSingle(inputs[i]);
                    i += 1;
                }

                for(i=1;i<layers.Count;i++)
                {
                    NeuronLayer nl=layers[i];
                    foreach(var someNeuron in nl)
                    {
                        someNeuron.UpdateOutput();
                    }
                }

                return GetOutput();
            }
            catch (System.Exception ex)
            {
                throw new RunningException("Error occurred while running the network. ", ex);
            }
        }

    }
}
