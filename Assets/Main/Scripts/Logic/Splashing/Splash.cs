using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Splashing
{
    public class Splash : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        [SerializeField] private float _endFadeValue;
        [SerializeField] private float _durationFade;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer.material.DOFade(_endFadeValue, _durationFade).OnComplete(() => Destroy(gameObject));
        }
    }
}