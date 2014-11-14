using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpNeuralEngine.cs
{
    public class PatternProcessingHelper
    {
        public List<object> ListFromPattern(String pattern)
        {
            List<object> arr = new List<object>();
            for (int counter = 0; counter < pattern.Length; counter++)
            {
                arr.Add(pattern[counter].ToString());
            }
            return arr;
        }

        public String PatternFromList(List<object> arr)
        {
            string str = "";
            for (int no = 0; no < arr.Count; no++)
            {
                str = str + Math.Round((double)arr[no]);
            }
            return str;
        }

        public int NumberFromList(List<object> arr)
        {
            return NumberFromPattern(PatternFromList(arr));
        }

        public char CharFromList(List<object> arr)
        {
            return (char)NumberFromPattern(PatternFromList(arr));
        }

        public List<object> ListFromChar(char ch)
        {
            return ListFromNumber((int)ch, 8);
        }

        public List<object> ListFromNumber(int value, int lengthOfList)
        {
            String pattern = PatternFromNumber(value, lengthOfList);

            List<object> arr= new List<object>();
            for(int counter=0; counter<pattern.Length;counter++)
            {
                arr.Add(pattern[counter].ToString());
            }
            return arr;

        }

        public int NumberFromPattern(String pattern)
        {
            int number = 0;
            int bit;
            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[pattern.Length - 1 - i].Equals('1'))
                    bit = 1;
                else
                    bit = 0;
                number = number + (((int)Math.Pow(2,i)) * bit);
            }
            return number;
        }

        public String PatternFromNumber(int number, int lengthOfPattern)
        {
            String pattern = "";
            int max = lengthOfPattern;
            for (int i = 0; i < max; i++)
            {
                if( ( ( (int)Math.Pow(2,i) ) & number ) == ( (int)Math.Pow(2,i) ) )
                    pattern = "1" + pattern;
                else
                    pattern = "0" + pattern;
            }
            return pattern;
        }
    }
}
