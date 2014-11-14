using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace EpNeuralEngine.cs
{
    public class NeuralEngineException:Exception
    {
        public NeuralEngineException(string message, Exception e):base(message,e)
        {
            Debug.Write("\r\n" + "----------" + "\r\n" + "Error: " + message + "\r\n" + "----------" + "\r\n" + e.StackTrace);
        }
    }
}
