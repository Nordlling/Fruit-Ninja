using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.BlockContainer;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BlockPiece : MonoBehaviour, IBlockable
    {
        public BlockMovement BlockMovement => _blockMovement;
        public BoundsChecker BoundsChecker => _boundsChecker;
        public BlockAnimation BlockAnimation => _blockAnimation;
        public Bounds ColliderBounds => _blockCollider.SphereBounds;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public ITimeProvider TimeProvider { get; protected set; }

        [SerializeField] private BlockMovement _blockMovement;
        [SerializeField] private BoundsChecker _boundsChecker;
        [SerializeField] private BlockAnimation _blockAnimation;
        [SerializeField] private BlockCollider _blockCollider;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SpriteRenderer _shadowSpriteRenderer;
        
        protected IBlockContainerService _blockContainerService;

        public float InvulnerabilityDuration { get; private set; }

        public void Construct(IBlockContainerService blockContainerService, ITimeProvider timeProvider, float invulnerabilityDuration)
        {
            _blockContainerService = blockContainerService;
            TimeProvider = timeProvider;
            InvulnerabilityDuration = invulnerabilityDuration;
        }

        private void Start()
        {
            if (_spriteRenderer != null)
            {
                _shadowSpriteRenderer.sprite = _spriteRenderer.sprite;
            }
        }

        private void Update()
        {
            if (InvulnerabilityDuration > 0)
            {
                InvulnerabilityDuration -= TimeProvider.GetDeltaTime();
            }
        }
        
        private void OnDestroy()
        {
            _blockContainerService?.RemoveBlock(this);
        }
    }
}