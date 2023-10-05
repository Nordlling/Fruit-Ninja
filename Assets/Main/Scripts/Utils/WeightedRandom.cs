using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Main.Scripts.Utils
{
    public static class WeightedRandom
    {
        public static int GetRandomWeightedIndex(List<float> items)
        {
            var weightSum = items.Sum();
            var value = Random.Range(0, weightSum);
            var current = 0f;

            for (int i = 0; i < items.Count; i++)
            {
                current += items[i];
       
                if (current > value)
                {
                    return i;
                }
            }

            throw new ArgumentException("Cannot find Item.");
        }
    }
}