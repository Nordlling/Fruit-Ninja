using Main.Scripts.Infrastructure.Provides;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BlockPiece : MonoBehaviour
    {
        public BlockMovement BlockMovement => _blockMovement;
        public BoundsChecker BoundsChecker => _boundsChecker;
        public BlockAnimation BlockAnimation => _blockAnimation;
        public BlockCollider BlockCollider => _blockCollider;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public ITimeProvider TimeProvider { get; protected set; }

        [SerializeField] private BlockMovement _blockMovement;
        [SerializeField] private BoundsChecker _boundsChecker;
        [SerializeField] private BlockAnimation _blockAnimation;
        [SerializeField] private BlockCollider _blockCollider;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SpriteRenderer _shadowSpriteRenderer;

        private void Start()
        {
            _shadowSpriteRenderer.sprite = _spriteRenderer.sprite;
        }

        public void Construct(ITimeProvider timeProvider)
        {
            TimeProvider = timeProvider;
        }
    }
}