using Main.Scripts.Infrastructure.Services.Freezing;
using UnityEngine;

namespace Main.Scripts.UI.Gameplay
{
    public class UIFreezeView : MonoBehaviour
    {
        [SerializeField] private FreezeBackgroundAnimation _freezeBackgroundAnimation;
        
        private IFreezeService _freezeService;

        public void Construct(IFreezeService freezeService)
        {
            _freezeService = freezeService;
        }

        private void OnEnable()
        {
            _freezeService.OnFreezed += FreezeBackground;
            _freezeService.OnUnfreezed += UnfreezeBackground;
        }

        private void OnDisable()
        {
            _freezeService.OnFreezed -= FreezeBackground;
            _freezeService.OnUnfreezed -= UnfreezeBackground;
        }
        
        private void FreezeBackground()
        {
            _freezeBackgroundAnimation.PlayFadeInAnimation();
        }
        
        private void UnfreezeBackground()
        {
            _freezeBackgroundAnimation.PlayFadeOutAnimation();
        }
        
    }
}