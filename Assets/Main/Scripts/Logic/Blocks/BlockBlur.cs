using DG.Tweening;
using Main.Scripts.Infrastructure.Services.Blurring;
using Main.Scripts.Utils.RectUtils;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BlockBlur : MonoBehaviour, IBlockable
    {
        public SlicedRect SlicedRect { get; set; }
        
        [SerializeField] private float _blurDuration;

        private IBlurService _blurService;
        private SpriteRenderer _blurredSpriteRenderer;
        private SpriteRenderer _originalSpriteRenderer;

        public void Construct(IBlurService blurService, SpriteRenderer blurredSpriteRenderer, SpriteRenderer originalSpriteRenderer)
        {
            _blurService = blurService;
            _blurredSpriteRenderer = blurredSpriteRenderer;
            _originalSpriteRenderer = originalSpriteRenderer;

            _blurService.OnBlurred += StartBlur;
            _blurService.OnUnblurred += FinishBlur;
        }

        private void Start()
        {
            if (_blurService.Blurred)
            {
                if (_blurredSpriteRenderer.sprite == null)
                {
                    CreateBlurSprite();
                }
                StartBlur();
            }
        }

        private void OnDisable()
        {
            _blurService.OnBlurred -= StartBlur;
            _blurService.OnUnblurred -= FinishBlur;
        }

        private void CreateBlurSprite()
        {
            if (SlicedRect != null)
            {
                _blurredSpriteRenderer.sprite = _blurService.BlurSprite(_originalSpriteRenderer.sprite.texture, SlicedRect.FirstRectPart, SlicedRect.FirstPivot);
            }
            else
            {
                _blurredSpriteRenderer.sprite = _blurService.BlurSprite(_originalSpriteRenderer.sprite.texture);
            }
        }

        private void StartBlur()
        {
            if (_blurredSpriteRenderer.sprite == null)
            {
                CreateBlurSprite();
            }
            _blurredSpriteRenderer.DOFade(1f, _blurDuration).SetLink(gameObject);
        }

        private void FinishBlur()
        {
            _blurredSpriteRenderer.DOFade(0f, _blurDuration).SetLink(gameObject);
        }
    }
}