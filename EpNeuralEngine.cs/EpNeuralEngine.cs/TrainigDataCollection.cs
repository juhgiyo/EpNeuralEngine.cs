using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpNeuralEngine.cs
{
    public class TrainigDataCollection:List<TrainingData>
    {
        public new TrainingData Add(TrainingData obj)
        {
            base.Add(obj);
            return obj;
        }

        public TrainingData Add()
        {
            TrainingData td = new TrainingData();
            base.Add(td);
            return td;
        }

        public new void Insert(int index, TrainingData obj)
        {
            base.Insert(index, obj);
        }

        public new void Remove(TrainingData obj)
        {
            base.Remove(obj);
        }

        public new TrainingData this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
            }
        }
    }
}
