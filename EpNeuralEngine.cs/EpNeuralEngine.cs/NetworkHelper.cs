using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EpNeuralEngine.cs
{
    public class NotInitializedException : NeuralEngineException
    {
        public NotInitializedException(string message, Exception e)
            : base(message, e)
        {
        }
    }

    public class NetworkHelperException : NeuralEngineException
    {
        public NetworkHelperException(string message, Exception e)
            : base(message, e)
        {
        }
    }
    public delegate void TrainingProgress(int currentRound, int maxRound,out bool cancel);

    public class NetworkHelper
    {
        public event TrainingProgress progress;

        private INeuralNetwork network=null;
        private TrainigDataCollection trainingQueue=new TrainigDataCollection();
        private bool ifTraining = false;

        public NetworkHelper()
        {

        }
        public NetworkHelper(INeuralNetwork network)
        {
            this.network = network;
            trainingQueue.Clear();
        }

        public void Initialize(INeuralNetwork network)
        {
            this.network = network;
            trainingQueue.Clear();
        }

        public void AddTrainingData(TrainingData data)
        {
            if (network == null)
                throw new NotInitializedException("Helper not yet initialized. Initialize the helper by calling the Initialize function first", null);
            TrainingDataQueue.Add(data);
        }

        public void AddTrainingData(List<object> input, List<object> output)
        {
            if (network == null)
                throw new NotInitializedException("Helper not yet initialized. Initialize the helper by calling the Initialize function first", null);
            if (input.Count != network.InputLayer.Count)
                throw new InvalidInputException("The number of input doesn't match the number of input layer neurons", null);
            if(output.Count!= network.OutputLayer.Count)
                throw new InvalidOutputException("The number of output doesn't match the number of output layer neurons", null);

            TrainingData td = new TrainingData(input, output);
            AddTrainingData(td);
        }
        public TrainigDataCollection TrainingDataQueue
        {
            get{
                return trainingQueue;    
            }
        }

        public void AddTrainingData(String input, String output)
        {
            List<object> inputList;
            List<object> outputList;

            PatternProcessingHelper ppHelper = new PatternProcessingHelper();
            inputList = ppHelper.ListFromPattern(input);
            outputList = ppHelper.ListFromPattern(output);

            AddTrainingData(inputList, outputList);
        }

        public void AddTrainingData(int input, int output)
        {
            if (network == null)
                throw new NotInitializedException("Helper not yet initialized. Initialize the helper by calling the Initialize function first", null);

            PatternProcessingHelper ppHelper = new PatternProcessingHelper();
            String inputPattern = ppHelper.PatternFromNumber(input, network.InputLayer.Count);
            String outputPattern = ppHelper.PatternFromNumber(output, network.OutputLayer.Count);

            AddTrainingData(inputPattern, outputPattern);
        }


        public void AddTrainingData(Image input, Image output)
        {
            if (network == null)
                throw new NotInitializedException("Helper not yet initialized. Initialize the helper by calling the Initialize function first", null);
            if (input.Width * input.Height != network.InputLayer.Count)
                throw new InvalidInputException("The number of pixels in input image doesn't match the number of input layer neurons", null);
            if (output.Width * output.Height != network.OutputLayer.Count)
                throw new InvalidOutputException("The number of pixels in output image doesn't match the number of output layer neurons", null);

            ImageProcessingHelper imgHelper = new ImageProcessingHelper();
            List<object> inArray = imgHelper.ListFromImage(input);
            List<object> outArray = imgHelper.ListFromImage(output);
            AddTrainingData(inArray, outArray);

        }


        public void AddTrainingData(Image input, List<object> output)
        {
            if (network == null)
                throw new NotInitializedException("Helper not yet initialized. Initialize the helper by calling the Initialize function first", null);
            if (input.Width * input.Height != network.InputLayer.Count)
                throw new InvalidInputException("The number of pixels in input image doesn't match the number of input layer neurons", null);

            ImageProcessingHelper imgHelper = new ImageProcessingHelper();
            List<object> inArray = imgHelper.ListFromImage(input);
            AddTrainingData(inArray, output);
        }

        public void AddTrainingData(Image input, String output)
        {
            if (network == null)
                throw new NotInitializedException("Helper not yet initialized. Initialize the helper by calling the Initialize function first", null);
            if (input.Width * input.Height != network.InputLayer.Count)
                throw new InvalidInputException("The number of pixels in input image doesn't match the number of input layer neurons", null);

            ImageProcessingHelper imgHelper = new ImageProcessingHelper();
            PatternProcessingHelper ppHelper = new PatternProcessingHelper();

            List<object> inArr = imgHelper.ListFromImage(input);
            List<object> outArr = ppHelper.ListFromPattern(output);

            AddTrainingData(inArr, outArr);
        }

        public void AddTrainingData(Image input, int output)
        {
            if (network == null)
                throw new NotInitializedException("Helper not yet initialized. Initialize the helper by calling the Initialize function first", null);
            if (input.Width * input.Height != network.InputLayer.Count)
                throw new InvalidInputException("The number of pixels in input image doesn't match the number of input layer neurons", null);
            
            ImageProcessingHelper imgHelper = new ImageProcessingHelper();
            PatternProcessingHelper ppHelper = new PatternProcessingHelper();

            List<object> inArr = imgHelper.ListFromImage(input);
            List<object> outArr = ppHelper.ListFromNumber(output, network.OutputLayer.Count);
            AddTrainingData(inArr, outArr);
        }

        public void Train(int rounds, bool breakOnError = true)
        {
            foreach (TrainingData td in TrainingDataQueue)
            {
                Util.PrintList("Data:", td.Inputs);
            }
            if (network == null)
                throw new NotInitializedException("Helper not yet initialized. Initialize the helper by calling the Initialize function first", null);
            if (ifTraining)
                throw new NetworkHelperException("Training is already going on", null);

            ifTraining = true;
            for (int i = 0; i < rounds; i++)
            {
                List<TrainingData> tempStates = TrainingDataQueue.ToList();
                do 
                {
                    try
                    {
                        int itemno = (int)(Util.RandomFloat() * (float)(tempStates.Count - 1));
                        TrainingData td = tempStates[itemno];
                        network.TrainNetwork(td);
                        tempStates.Remove(td);
                    }
                    catch (System.Exception ex)
                    {
                        ifTraining = false;
                        throw new NetworkHelperException("An error occurred while training", ex);
                    }
                } while (tempStates.Count>0);

                bool cancel = false;
                progress(i + 1, rounds, out cancel);
                if(cancel)
                    break;

            }
            ifTraining = false;

        }

        public void ClearTrainingData()
        {
            this.trainingQueue.Clear();
        }

    }
}
