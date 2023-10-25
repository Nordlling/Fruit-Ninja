using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Gameplay
{
    public class FreezeBackgroundAnimation : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private float _fadeInAlpha;
        [SerializeField] private float _animationDuration;

        private const float _fadeOutAlpha = 0f;

        public void PlayFadeInAnimation()
        {
            ResetValues();
            
            DOTween.Sequence()
                .Append(_backgroundImage.DOFade(_fadeInAlpha, _animationDuration))
                .Play();
        }

        public void PlayFadeOutAnimation()
        {
            DOTween.Sequence()
                .Append(_backgroundImage.DOFade(_fadeOutAlpha, _animationDuration))
                .Play();
        }

        private void ResetValues()
        {
            Color color = _backgroundImage.color;
            color.a = 0;
            _backgroundImage.color = color;
        }
    }
}