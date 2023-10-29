using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Gameplay
{
    public class PauseAnimation : MonoBehaviour
    {
        [SerializeField] private float _animationDuration;
        [SerializeField] private GameObject _pauseContent;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _blurImage;
        
        public void PlayFadeInAnimation()
        {
            ResetValues();
            _blurImage.DOFade(1f, _animationDuration);
            _backgroundImage.DOFade(0.8f, _animationDuration);
            DOTween.Sequence()
                .Append(_pauseContent.transform.DOScale(1.2f, _animationDuration))
                .Append(_pauseContent.transform.DOScale(0.8f, 0.2f))
                .Append(_pauseContent.transform.DOScale((1f), 0.1f));
        }

        public void PlayFadeOutAnimation(Action onFadeEnd)
        {
            _blurImage.DOFade(0f, _animationDuration);
            _backgroundImage.DOFade(0f, _animationDuration);
            _pauseContent.transform.DOScale(0f, _animationDuration).OnKill(() => onFadeEnd?.Invoke());
        }

        private void ResetValues()
        {
            _pauseContent.transform.localScale = Vector3.zero;
        }
    }
}