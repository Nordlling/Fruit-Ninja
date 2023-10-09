using Main.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BoundsChecker : MonoBehaviour
    {
        private LivingZone _livingZone;

        private void Start()
        {
            _livingZone = ServiceContainer.Instance.Get<LivingZone>();
        }

        private void Update()
        {
            if (!_livingZone.IsInDeadZone(transform.position))
            {
                Destroy(gameObject);
            }
        }
    }
}
