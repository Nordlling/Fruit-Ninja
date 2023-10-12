using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Splash
{
    public class Splash : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _sprites;
        private void Start()
        {
            int randomIndex = Random.Range(0, _sprites.Length);
            _spriteRenderer.sprite = _sprites[randomIndex];
            
            _spriteRenderer.material.DOFade(0f, 2f).OnComplete(() => Destroy(gameObject));
        }
    }
}