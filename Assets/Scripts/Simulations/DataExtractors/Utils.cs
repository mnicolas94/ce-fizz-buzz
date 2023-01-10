using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulations.DataExtractors
{
    public static class Utils
    {
        public static (int, int, int) GetMinMaxMean(List<int> values)
        {
            int min = Int32.MaxValue;
            int max = Int32.MinValue;
            int mean = 0;
            foreach (var value in values)
            {
                min = Math.Min(min, value);
                max = Math.Max(max, value);
                mean += value;
            }

            mean /= values.Count;

            return (min, max, mean);
        }
        
        public static  (float, float, float) GetMinMaxMean(List<float> values)
        {
            float min = Int32.MaxValue;
            float max = Int32.MinValue;
            float mean = 0;
            foreach (var value in values)
            {
                min = Mathf.Min(min, value);
                max = Mathf.Max(max, value);
                mean += value;
            }

            mean /= values.Count;

            return (min, max, mean);
        }
        
        public static string GetMinMaxMeanString(List<int> values)
        {
            var (min, max, mean) = GetMinMaxMean(values);
            return $"{mean}({min}-{max})";
        }
        
        public static string GetMinMaxMeanString(List<float> values)
        {
            var (min, max, mean) = GetMinMaxMean(values);
            return $"{mean}({min}-{max})";
        }
    }
}