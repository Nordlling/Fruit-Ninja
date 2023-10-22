using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Main.Scripts.UI.Gameplay
{
    public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] private Button _button;
        [SerializeField] private Color _pressedColor;
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _buttonPressedScale;

        private Color _originalColor;
        private float _originalScale;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_button.interactable)
            {
                return;
            }
            
            AnimatePressedButton();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_button.interactable)
            {
                return;
            }
            
            AnimateUnpressedButton();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_button.interactable)
            {
                return;
            }
            
            DOTween.Sequence().Append(AnimatePressedButton()).Append(AnimateUnpressedButton()).Play();
        }

        private void Start()
        {
            _originalColor = _button.image.color;
            _originalScale = 1f;
        }

        private Sequence AnimatePressedButton()
        {
            return DOTween
                .Sequence()
                .Join(transform.DOScale(_buttonPressedScale, _animationDuration))
                .Join(_button.image.DOColor(_pressedColor, _animationDuration));
        }

        private Sequence AnimateUnpressedButton()
        {
            return DOTween
                .Sequence()
                .Join(transform.DOScale(_originalScale, _animationDuration))
                .Join(_button.image.DOColor(_originalColor, _animationDuration));
        }
    }
}