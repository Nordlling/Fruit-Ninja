using System;
using DG.Tweening;
using Main.Scripts.Infrastructure.States;
using Main.Scripts.UI.Loading;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Menu
{
    public class StartGameAnimation : MonoBehaviour
    {
        [SerializeField] private Image _backgroundBlur;
        [SerializeField] private Image _leftHouseImage;
        [SerializeField] private Image _rightHouseImage;
        [SerializeField] private Image _lightImage;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _canvasGroupAnimationDuration;
        [SerializeField] private float _blurAnimationDuration;
        [SerializeField] private float _housesAnimationDuration;
        [SerializeField] private float _housesScaleTarget;

        private const float _pause = 0.2f;

        public void StartAnimation(Action onFinished)
        {
            Vector2 housesRectWidth = new Vector2(_leftHouseImage.rectTransform.rect.width, 0f) * _housesScaleTarget;
            Vector2 lightRectHeight = new Vector2(0f, _lightImage.rectTransform.rect.height) * _housesScaleTarget;

            DOTween.Sequence()
                .AppendInterval(_pause)
                .Append(_leftHouseImage.rectTransform.DOAnchorPos(-housesRectWidth, _housesAnimationDuration))
                .Join(_leftHouseImage.rectTransform.DOScale(_housesScaleTarget, _housesAnimationDuration))
                .Join(_rightHouseImage.rectTransform.DOAnchorPos(housesRectWidth, _housesAnimationDuration))
                .Join(_rightHouseImage.rectTransform.DOScale(_housesScaleTarget, _housesAnimationDuration))
                .Join(_lightImage.rectTransform.DOAnchorPos(lightRectHeight, _housesAnimationDuration))
                .Join(_lightImage.rectTransform.DOScale(_housesScaleTarget, _housesAnimationDuration))
                .Join(_canvasGroup.DOFade(0f, _canvasGroupAnimationDuration))
                .Join(_backgroundBlur.DOFade(0f, _blurAnimationDuration).OnComplete(() => onFinished?.Invoke()))
                .Play();
        }
        
    }
}
