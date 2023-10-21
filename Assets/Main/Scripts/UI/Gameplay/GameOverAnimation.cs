using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Gameplay
{
    public class GameOverAnimation : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private TextMeshProUGUI _loseText;
        [SerializeField] private CanvasGroup _scoreCanvasGroup;
        [SerializeField] private CanvasGroup _buttonsCanvasGroup;

        public void PlayFadeInAnimation()
        {
            ResetValues();
            
            DOTween.Sequence()
                .Append(_backgroundImage.DOFade(0.9f, 2f))
                .Append(_loseText.DOFade(1f, 1f))
                .Append(_scoreCanvasGroup.DOFade(1f, 1f))
                .Join(_buttonsCanvasGroup.DOFade(1f, 1f))
                .Play();
        }

        public void PlayFadeOutAnimation(Action OnFinished)
        {
            DOTween.Sequence()
                .Append(_backgroundImage.DOFade(0f, 2f))
                .Join(_loseText.DOFade(0f, 2f))
                .Join(_scoreCanvasGroup.DOFade(0f, 2f))
                .Join(_buttonsCanvasGroup.DOFade(0f, 2f))
                .OnKill(() => OnFinished?.Invoke())
                .Play();
        }

        private void ResetValues()
        {
            Color color = _backgroundImage.color;
            color.a = 0;
            _backgroundImage.color = color;
            
            _loseText.alpha = 0f;
            _scoreCanvasGroup.alpha = 0f;
            _buttonsCanvasGroup.alpha = 0f;
        }
    }
}