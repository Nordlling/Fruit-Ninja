using DG.Tweening;
using UnityEngine;

namespace Main.Scripts.UI.Gameplay
{
    public class SamuraiTimerAnimation : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _animationDuration;
        
        public void PlayFadeInAnimation()
        {
            _canvasGroup.alpha = 0f;
            
            DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1f, _animationDuration))
                .Play();
        }

        public void PlayFadeOutAnimation()
        {
            DOTween.Sequence()
                .Append(_canvasGroup.DOFade(0f, _animationDuration))
                .Play();
        }
    }
}