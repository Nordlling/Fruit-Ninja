using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.LivingZone;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BoundsChecker : MonoBehaviour
    {
        private LivingZone _livingZone;

        public void Construct(LivingZone livingZone)
        {
            _livingZone = livingZone;
        }

        private void Update()
        {
            if (!_livingZone.IsInLivingZone(transform.position))
            {
                Destroy(gameObject);
            }
        }
    }
}
