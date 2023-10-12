using Main.Scripts.Infrastructure.Services.Collision;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Blocks
{
    public class Block : MonoBehaviour
    {
        public BlockMovement BlockMovement => _blockMovement;
        public BoundsChecker BoundsChecker => _boundsChecker;
        public BlockCollider BlockCollider => _blockCollider;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Slicer Slicer => _slicer;

        [SerializeField] private BlockMovement _blockMovement;
        [SerializeField] private BoundsChecker _boundsChecker;
        [SerializeField] private BlockCollider _blockCollider;
        [SerializeField] private Slicer _slicer;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SpriteRenderer _shadowSpriteRenderer;
        [SerializeField] private Sprite[] _sprites;
        
        private ICollisionService _collisionService;
        
        public void Construct(ICollisionService collisionService)
        {
            _collisionService = collisionService;
        }

        private void Awake()
        {
            int randomIndex = Random.Range(0, _sprites.Length);
            _spriteRenderer.sprite = _sprites[randomIndex];
        }

        private void Start()
        {
            _shadowSpriteRenderer.sprite = _spriteRenderer.sprite;
        }

        private void OnDestroy()
        {
            _collisionService?.RemoveBlock(this);
        }
    }
}