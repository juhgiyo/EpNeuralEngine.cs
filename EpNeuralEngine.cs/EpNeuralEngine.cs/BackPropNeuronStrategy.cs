using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpNeuralEngine.cs
{
    public class NeuronStrategyException : NeuralEngineException
    {
        public NeuronStrategyException(string message, Exception e)
            : base(message, e)
        {
        }
    }

    public class BackPropNeuronStrategy : INeuronStrategy
    {
        public Single FindDelta(Single output, Single errorFactor)
        {
            try
            {
                return output * (1 - output) * errorFactor;
            }
            catch (System.Exception ex)
            {
                throw new NeuronStrategyException("Exception in Finding Delta", ex);
            }
        }

        public Single Activation(Single value)
        {
            try
            {
                return (1 / (1 + (Single)Math.Exp(value * -1)));
            }
            catch (System.Exception ex)
            {
                throw new NeuronStrategyException("Exception in Activation function", ex);
            }
        }

        public Single FindNetValue(NeuronConnections Inputs, Single bias)
        {
            try
            {
                Single sum = bias;
                foreach (var nur in Inputs.Neurons())
                {
                    sum = sum + (Inputs[nur] * nur.OutputValue);
                }
                return sum;
            }
            catch (System.Exception ex)
            {
                throw new NeuronStrategyException("Exception in Finding Net Value", ex);
            }
            
        }

        public Single FindNewBias(Single bias, Single delta)
        {
            try
            {
                return bias + Global.LEARNING_RATE * delta;
            }
            catch (System.Exception ex)
            {
                throw new NeuronStrategyException("Exception in Finding New Bias Value", ex);
            }
        }

        public void UpdateWeights(NeuronConnections connections, Single delta)
        {
            try
            {
                for (int neuronTrav=0;neuronTrav<connections.Neurons().Count;neuronTrav++)
                {
                    INeuron nur = connections.Neurons().ElementAt(neuronTrav);
                    connections[nur] = connections[nur] + Global.LEARNING_RATE * nur.OutputValue * delta;
                }
            }
            catch (System.Exception ex)
            {
                throw new NeuronStrategyException("Exception while updating the weight values", ex);
            }
        }
    }
}
