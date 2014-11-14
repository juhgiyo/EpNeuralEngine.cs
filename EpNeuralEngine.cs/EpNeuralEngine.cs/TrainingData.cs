using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpNeuralEngine.cs
{
    public class TrainingData
    {
        List<object> inputs = new List<object>();
        List<object> outputs = new List<object>();

        public List<object> Inputs
        {
            get { return inputs; }
            protected set { inputs = value; }
        }

        public List<object> Outputs
        {
            get { return outputs; }
            protected set { outputs = value; }
        }

        public TrainingData()
        {
            inputs = new List<object>();
            outputs = new List<object>();
        }

        public TrainingData(List<object> inputs, List<object> outputs)
        {
            this.inputs = inputs;
            this.outputs = outputs;
        }

    }
}
