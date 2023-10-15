using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.Health;
using Main.Scripts.Infrastructure.Services.LivingZone;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BoundsChecker : MonoBehaviour
    {
        private LivingZone _livingZone;
        private IHealthService _healthService;
        private bool _healthAffect;

        public void Construct(LivingZone livingZone, IHealthService healthService,  bool healthAffect)
        {
            _livingZone = livingZone;
            _healthService = healthService;
            _healthAffect = healthAffect;
        }

        private void Update()
        {
            if (!_livingZone.IsInLivingZone(transform.position))
            {
                if (_healthAffect)
                {
                    _healthService.DecreaseHealth();
                }
                Destroy(gameObject);
            }
        }
    }
}
