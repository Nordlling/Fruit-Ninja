using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Main.Scripts.Utils.RandomUtils
{
    public static class WeightedRandomExtension
    {
        public static int GetRandomWeightedIndex(this IList<float> items)
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

            return -1;
        }
    }
}