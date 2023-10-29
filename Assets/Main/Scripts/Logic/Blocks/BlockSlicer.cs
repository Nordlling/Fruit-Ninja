using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services.Combo;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Logic.Label;
using Main.Scripts.Utils.RectUtils;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BlockSlicer : MonoBehaviour, ISlicer
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ScoreLabel _scoreLabel;
        [SerializeField] private float _departureSpeed;
        [SerializeField] private float _rectPieceDirectionAngle;

        private IBlockFactory _blockFactory;
        private ILabelFactory _labelFactory;
        private ISliceEffectFactory _sliceEffectFactory;
        private IScoreService _scoreService;
        private IComboService _comboService;
        private int _visualIndex;

        public void Construct(
            IBlockFactory blockFactory, 
            ILabelFactory labelFactory,
            ISliceEffectFactory sliceEffectFactory,
            IScoreService scoreService, 
            IComboService comboService,
            int visualIndex)
        {
            _blockFactory = blockFactory;
            _labelFactory = labelFactory;
            _sliceEffectFactory = sliceEffectFactory;
            _scoreService = scoreService;
            _comboService = comboService;
            _visualIndex = visualIndex;
        }
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            int addedScore = _scoreService.AddScore();
            _comboService.AddComboScore(transform.position);

            _labelFactory.CreateScoreLabel(_scoreLabel, transform.position, addedScore.ToString());
            
            Sprite originalSprite = _spriteRenderer.sprite;
            
            SlicedRect slicedRect = CreateSlicedRect(swiperPosition, swiperDirection, originalSprite);
            
            CreateSpritePart(originalSprite, slicedRect.FirstRectPart, slicedRect.FirstRectPartDirection, slicedRect.FirstPivot);
            CreateSpritePart(originalSprite, slicedRect.SecondRectPart, slicedRect.SecondRectPartDirection, slicedRect.SecondPivot);

            SpawnSplash();

            Destroy(gameObject);
        }

        private SlicedRect CreateSlicedRect(Vector2 swiperPosition, Vector2 swiperDirection, Sprite originalSprite)
        {
            Rect rect = new Rect(0f, 0f, originalSprite.texture.width, originalSprite.texture.height);
            
            Quaternion inversedRotation = Quaternion.Inverse(transform.rotation);

            Vector2 worldDirectionFromCenter = swiperPosition - (Vector2)transform.position;
            Vector2 rectDirectionFromCenter = worldDirectionFromCenter * (rect.size / _spriteRenderer.size);
            Vector2 rectDirectionFromCenterRotated = inversedRotation * rectDirectionFromCenter;
            Vector2 rectPoint = rect.center + rectDirectionFromCenterRotated;

            Vector2 swiperDirectionInversedRotated = inversedRotation * swiperDirection;
            
            SlicedRect slicedRect = rect.GetSlicedRect(rectPoint, swiperDirectionInversedRotated);
            
            slicedRect.FirstRectPartDirection = Quaternion.Euler(0, 0, -_rectPieceDirectionAngle) * swiperDirection.normalized;
            slicedRect.SecondRectPartDirection = Quaternion.Euler(0, 0, +_rectPieceDirectionAngle) * swiperDirection.normalized;
            
            return slicedRect;
        }

        private void CreateSpritePart(Sprite originalSprite, Rect rectPart, Vector2 rectPartDirection, Vector2 pivot)
        {
            Sprite part = Sprite.Create(
                originalSprite.texture,
                rectPart,
                pivot);

            BlockPiece blockPiece = _blockFactory.CreateBlockPiece(transform.position);
            blockPiece.transform.rotation = transform.rotation;
            blockPiece.transform.localScale = transform.localScale;
            blockPiece.BlockMovement.Construct(rectPartDirection, _departureSpeed, blockPiece.TimeProvider);
            blockPiece.SpriteRenderer.sprite = part;
            blockPiece.BlockBlur.SlicedRect = new SlicedRect{ FirstRectPart = rectPart, FirstPivot = pivot };
        }

        private void SpawnSplash()
        {
            _sliceEffectFactory.CreateBlockSplash(transform.position, _visualIndex);
        }
    }
}