using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpNeuralEngine.cs
{
    public class StrategyInitializedException : NeuralEngineException
    {
        public StrategyInitializedException(string message, Exception e):base(message,e)
        {
        }
    }

    public class Neuron:INeuron
    {
        Single bias = Util.RandomFloat();
        Single output;
        Single delta;
        NeuronCollection forwardConnections = new NeuronCollection();
        NeuronConnections inputs = new NeuronConnections();
        INeuronStrategy strategy = null;

        public Single BiasValue
        {
            get { 
                return bias; 
            }
            set { 
                bias = value; 
            }
        }
        public Single OutputValue
        {
            get
            {
                return output;
            }
            set
            {
                output = value;
            }
        }
        public Single DeltaValue
        {
            get
            {
                return delta;
            }
            set
            {
                delta = value;
            }
        }
        public NeuronCollection ForwardConnections
        {
            get
            {
                return forwardConnections;
            }
        }
        public NeuronConnections Inputs
        {
            get
            {
                return inputs;
            }
            
        }
        public INeuronStrategy Strategy
        {
            get
            {
                return strategy;
            }
            set
            {
                strategy = value;
            }
        }

        public Neuron()
        {

        }

        public Neuron(INeuronStrategy strategy)
        {
            this.strategy = strategy;
        }


        public void UpdateDelta(Single errorFactor)
        {
            if (strategy == null)
                throw new StrategyInitializedException("Strategy of the neuron not initialized. Assign a proper strategy", null);
            DeltaValue = Strategy.FindDelta(OutputValue, errorFactor);
        }

        public void UpdateOutput()
        {
            if (strategy == null)
                throw new StrategyInitializedException("Strategy of the neuron not initialized. Assign a proper strategy", null);
            Single netValue = Strategy.FindNetValue(Inputs, BiasValue);
            OutputValue = Strategy.Activation(netValue);
        }

        public void UpdateFreeParams()
        {
            if (strategy == null)
                throw new StrategyInitializedException("Strategy of the neuron not initialized. Assign a proper strategy", null);
            BiasValue = Strategy.FindNewBias(BiasValue, DeltaValue);
            Strategy.UpdateWeights(Inputs, DeltaValue);
        }


    }
}
