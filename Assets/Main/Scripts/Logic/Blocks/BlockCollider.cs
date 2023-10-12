using System;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BlockCollider : MonoBehaviour
    {
        [SerializeField] private float _sphereScale;
        
        public Bounds SphereBounds { get; private set; }
        private Vector3 _sphereCenter;
        private float _sphereRadius;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Sprite _originalSprite;

        public void Slice()
        {
            Debug.Log("Destroyed");
            
            Sprite leftSprite = Sprite.Create(_originalSprite.texture, new Rect(0, 0, _originalSprite.texture.width / 2, _originalSprite.texture.height), new Vector2(0.5f, 0.5f));
            _spriteRenderer.sprite = leftSprite;
            // Destroy(gameObject);
        }
        private void Start()
        {
            _originalSprite = _spriteRenderer.sprite;
            _sphereCenter = transform.position;
            _sphereRadius = transform.localScale.x * _sphereScale;
            SphereBounds = new Bounds(_sphereCenter, new Vector3(_sphereRadius, _sphereRadius, _sphereRadius));  
        }

        private void Update()
        {
            UpdateBounds();
        }

        private void UpdateBounds()
        {
            var sphereBounds = SphereBounds;
            sphereBounds.center = transform.position;
            SphereBounds = sphereBounds;
        }

        private void OnDrawGizmos()
        {
            _sphereCenter = transform.position;
            _sphereRadius = transform.localScale.x * _sphereScale;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_sphereCenter, _sphereRadius / 2f);
        }
    }
}