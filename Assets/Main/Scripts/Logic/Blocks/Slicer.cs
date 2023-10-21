using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services.Combo;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Logic.Score;
using Main.Scripts.Logic.Splashing;
using Main.Scripts.Utils.RectUtils;
using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class Slicer : MonoBehaviour, ISlicer
    {
        [SerializeField] private float _departureSpeed;
        [SerializeField] private float _rectPieceDirectionAngle;
        
        [SerializeField] private BlockPiece _blockPiecePrefab;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Splash _splashPrefab;
        
        [SerializeField] private ScoreLabel _scoreLabel;
        
        private IGameFactory _gameFactory;
        private ILabelFactory _labelFactory;
        private IScoreService _scoreService;
        private IComboService _comboService;
        private Sprite _splashSprite;

        public void Construct(IGameFactory gameFactory, ILabelFactory labelFactory, IScoreService scoreService, IComboService comboService, Sprite splashSprite)
        {
            _gameFactory = gameFactory;
            _labelFactory = labelFactory;
            _scoreService = scoreService;
            _comboService = comboService;
            _splashSprite = splashSprite;
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

            BlockPiece blockPiece = _gameFactory.CreateBlockPiece(_blockPiecePrefab, transform.position);
            blockPiece.transform.rotation = transform.rotation;
            blockPiece.transform.localScale = transform.localScale;
            blockPiece.BlockMovement.Construct(rectPartDirection, _departureSpeed, blockPiece.TimeProvider);
            blockPiece.SpriteRenderer.sprite = part;
        }

        private void SpawnSplash()
        {
            Splash splash = _gameFactory.CreateSplash(_splashPrefab, transform.position);
            splash.SpriteRenderer.sprite = _splashSprite;
        }
    }
}