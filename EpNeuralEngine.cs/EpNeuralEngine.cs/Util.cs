using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace EpNeuralEngine.cs
{
    public class Util
    {
        private static Random random = new Random();
        public static double RandomDouble()
        {
            var result = random.NextDouble();
            return (double)result;
        }
        public static float RandomFloat()
        {
            var result = random.NextDouble();
            return (float)result;
        }

        public static Single RandomSingle()
        {
            var result = random.NextDouble();
            return (float)result;
        }

        public static int RandomInt()
        {
            var result = random.Next();
            return (int)result;
        }

        public static void PrintList(String msg, List<object> arr)
        {
            string str = "";
            for (int ar = 0; ar < arr.Count; ar++)
            {
                str = str + arr[ar];
            }
            Debug.WriteLine(msg + " - " + str);
        }

        public static void Report(ListView view, INeuralNetwork network)
        {
            try
            {
                view.Items.Clear();
                view.Columns.Clear();

                view.Columns.Add("Neuron", 100, HorizontalAlignment.Left);
                view.Columns.Add("Bias", 60, HorizontalAlignment.Left);
                view.Columns.Add("Input Weights", 120, HorizontalAlignment.Left);
                view.Columns.Add("Delta", 60, HorizontalAlignment.Left);
                view.Columns.Add("Output", 160, HorizontalAlignment.Left);

                view.View = View.Details;

                string inputs;

                NeuronLayer layer = network.Layers[0];
                int i = 0;

                foreach(INeuron myNeuron in layer)
                {
                    inputs = "";
                    foreach (INeuron con in myNeuron.Inputs.Neurons())
                    {
                        inputs = inputs + myNeuron.Inputs[con].ToString("0.00")+", ";
                    }

                    ListViewItem viewItem = view.Items.Add("input" + i);
                    viewItem.SubItems.Add(myNeuron.BiasValue.ToString("0.00"));
                    viewItem.SubItems.Add(inputs);
                    viewItem.SubItems.Add(myNeuron.DeltaValue.ToString("0.00"));
                    viewItem.SubItems.Add(myNeuron.OutputValue.ToString("0.00"));
                    i += 1;

                }

                i = 0;
                int j = 0;
                for (int layercount = 1; layercount < network.Layers.Count - 1; layercount++)
                {
                    NeuronLayer nl = network.Layers[layercount];

                    foreach (INeuron myNeuron in nl)
                    {
                        inputs = "";
                        foreach (INeuron con in myNeuron.Inputs.Neurons())
                        {
                            inputs = inputs + myNeuron.Inputs[con].ToString("0.00") + ", ";
                        }

                        ListViewItem viewItem = view.Items.Add("Hidden" + i+j);
                        viewItem.SubItems.Add(myNeuron.BiasValue.ToString("0.00"));
                        viewItem.SubItems.Add(inputs);
                        viewItem.SubItems.Add(myNeuron.DeltaValue.ToString("0.00"));
                        viewItem.SubItems.Add(myNeuron.OutputValue.ToString("0.00"));
                        i += 1;
                    }
                    j += 1;
                }

                i = 0;
                foreach (INeuron myNeuron in network.Layers[network.Layers.Count - 1])
                {
                    inputs = "";
                    foreach (INeuron con in myNeuron.Inputs.Neurons())
                    {
                        inputs = inputs + myNeuron.Inputs[con].ToString("0.00") + ", ";
                    }
                    ListViewItem viewItem = view.Items.Add("Output" + i);
                    viewItem.SubItems.Add(myNeuron.BiasValue.ToString("0.00"));
                    viewItem.SubItems.Add(inputs);
                    viewItem.SubItems.Add(myNeuron.DeltaValue.ToString("0.00"));
                    viewItem.SubItems.Add(myNeuron.OutputValue.ToString("0.00"));
                    i += 1;

                }

            }
            catch (System.Exception ex)
            {
                throw new NeuralEngineException("Unable to complete reporting. Error in Report function.", ex);
            }
        }
    }
}
