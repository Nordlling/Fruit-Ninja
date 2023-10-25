using Main.Scripts.Infrastructure.Configs.Boosters;
using Main.Scripts.Infrastructure.Factory;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks.BlockBag
{
    public class BlockBagSlicer : MonoBehaviour, ISlicer
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private IBlockFactory _blockFactory;
        private BlockBagConfig _blockBagConfig;
        private ISliceEffectFactory _sliceEffectFactory;
        private int _visualIndex;

        public void Construct(IBlockFactory blockFactory, BlockBagConfig blockBagConfig, ISliceEffectFactory sliceEffectFactory, int visualIndex)
        {
            _blockFactory = blockFactory;
            _sliceEffectFactory = sliceEffectFactory;
            _blockBagConfig = blockBagConfig;
            _visualIndex = visualIndex;
        }
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            int blockCount = Random.Range(_blockBagConfig.MinBlockCount, _blockBagConfig.MaxBlockCount);

            for (int i = 0; i < blockCount; i++)
            {
                float speed = Random.Range(_blockBagConfig.MinSpeed, _blockBagConfig.MaxSpeed);
                float angle = Random.Range(-_blockBagConfig.LeftAngle, _blockBagConfig.RightAngle);
                Vector2 startDirection = Quaternion.Euler(0, 0, -angle) * Vector2.up;
                Block block = _blockFactory.CreateBlock(transform.position, _blockBagConfig.InvulnerabilityDuration);
                block.BlockMovement.Construct(startDirection, speed, block.TimeProvider);
            }

            SpawnParts();

            SpawnSplash();
            
            Destroy(gameObject);
        }

        private void SpawnParts()
        {
            Vector2 leftPartDirection = Quaternion.Euler(0, 0, _blockBagConfig.PartAngle) * Vector2.up;
            Vector2 rightPartDirection = Quaternion.Euler(0, 0, -_blockBagConfig.PartAngle) * Vector2.up;
            Vector2 leftPartPosition = (Vector2)transform.position + Vector2.left;
            Vector2 rightPartPosition = (Vector2)transform.position + Vector2.right;
            
            SpawnBlockPiece(_blockBagConfig.LeftPart, leftPartPosition, leftPartDirection);
            SpawnBlockPiece(_blockBagConfig.RightPart, rightPartPosition, rightPartDirection);
        }

        private void SpawnBlockPiece(Sprite sprite, Vector2 position, Vector2 direction)
        {
            
            BlockPiece blockPiece = _blockFactory.CreateBlockPiece(position);
            blockPiece.BlockMovement.Construct(direction, _blockBagConfig.PartSpeed, blockPiece.TimeProvider);
            blockPiece.SpriteRenderer.sprite = sprite;
            blockPiece.SpriteRenderer.transform.localScale = _spriteRenderer.transform.localScale;
        }
        
        private void SpawnSplash()
        {
            _sliceEffectFactory.CreateBlockBagSplash(transform.position, _visualIndex);
        }
    }
}