using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Blocks
{
    public class Block : MonoBehaviour
    {
        public BlockMovement BlockMovement => _blockMovement;
        public BoundsChecker BoundsChecker => _boundsChecker;

        [SerializeField] private BlockMovement _blockMovement;
        [SerializeField] private BoundsChecker _boundsChecker;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _sprites;

        private void Start()
        {
            int randomIndex = Random.Range(0, _sprites.Length);
            _spriteRenderer.sprite = _sprites[randomIndex];
        }
    }
}