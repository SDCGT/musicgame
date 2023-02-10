using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NDtw.Preprocessing
{
    public interface IPreprocessor
    {
        double[] Preprocess(double[] data);
        string ToString();
    }
}
