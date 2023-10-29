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
        [SerializeField] private Image _blurImage;

        private const float _fadeOutAlpha = 0f;

        public void PlayFadeInAnimation()
        {
            ResetValues();
            
            _blurImage.DOFade(1f, _animationDuration);
            DOTween.Sequence()
                .Append(_backgroundImage.DOFade(_fadeInAlpha, _animationDuration))
                .Play();
        }

        public void PlayFadeOutAnimation()
        {
            _blurImage.DOFade(0f, _animationDuration);
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