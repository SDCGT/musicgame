using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

namespace NDtw.Preprocessing
{
    public class NonePreprocessor : IPreprocessor
    {
        public double[] Preprocess(double[] data)
        {
            return data;
        }

        public override string ToString()
        {
            return "None";
        }
    }
}
