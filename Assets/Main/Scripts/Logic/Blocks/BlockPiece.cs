using System;
using Main.Scripts.Infrastructure.Services;
using Main.Scripts.Infrastructure.Services.LivingZone;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Blocks
{
    public class BlockPiece : MonoBehaviour
    {
        public BlockMovement BlockMovement => _blockMovement;
        public BoundsChecker BoundsChecker => _boundsChecker;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        [SerializeField] private BlockMovement _blockMovement;
        [SerializeField] private BoundsChecker _boundsChecker;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SpriteRenderer _shadowSpriteRenderer;

        private void Start()
        {
            _shadowSpriteRenderer.sprite = _spriteRenderer.sprite;
        }
    }
}