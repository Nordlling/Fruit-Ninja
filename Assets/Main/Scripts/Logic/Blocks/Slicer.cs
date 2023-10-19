using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Services.Score;
using Main.Scripts.Logic.Score;
using Main.Scripts.Logic.Splashing;
using Main.Scripts.Utils.RectUtils;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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
        private IScoreService _scoreService;
        private Sprite _splashSprite;

        public void Construct(IGameFactory gameFactory, IScoreService scoreService, Sprite splashSprite)
        {
            _gameFactory = gameFactory;
            _scoreService = scoreService;
            _splashSprite = splashSprite;
        }
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            int addedScore = _scoreService.AddScore();

            ScoreLabel scoreLabel = Instantiate(_scoreLabel, transform.position, Quaternion.identity);
            scoreLabel.Construct(addedScore.ToString());
            
            Sprite originalSprite = _spriteRenderer.sprite;
            
            SlicedRect slicedRect = CreateSlicedRect(swiperPosition, swiperDirection, originalSprite);

            CreateSpritePart(originalSprite, slicedRect.FirstRectPart, slicedRect.FirstRectPartDirection);
            CreateSpritePart(originalSprite, slicedRect.SecondRectPart, slicedRect.SecondRectPartDirection);

            SpawnSplash();

            Destroy(gameObject);
        }

        private SlicedRect CreateSlicedRect(Vector2 swiperPosition, Vector2 swiperDirection, Sprite originalSprite)
        {
            Rect rect = new Rect(0f, 0f, originalSprite.texture.width, originalSprite.texture.height);
            Vector2 worldDirection = swiperPosition - (Vector2)transform.position;
            Vector2 rectDirection = worldDirection * (rect.size / _spriteRenderer.size);
            Vector2 rectPoint = rect.center + rectDirection;
            
            SlicedRect slicedRect = rect.GetSlicedRect(rectPoint, swiperDirection, _rectPieceDirectionAngle);
            return slicedRect;
        }

        private void CreateSpritePart(Sprite originalSprite, Rect rectPart, Vector2 rectPartDirection)
        {
            Sprite part = Sprite.Create(
                originalSprite.texture,
                rectPart,
                new Vector2(0.5f, 0.5f));

            BlockPiece blockPiece = _gameFactory.CreateBlockPiece(_blockPiecePrefab, transform.position);
            blockPiece.transform.rotation = transform.rotation;
            blockPiece.transform.localScale = transform.localScale;
            blockPiece.BlockMovement.Construct(rectPartDirection, _departureSpeed);
            blockPiece.SpriteRenderer.sprite = part;
        }

        private void SpawnSplash()
        {
            Splash splash = _gameFactory.CreateSplash(_splashPrefab, transform.position);
            splash.SpriteRenderer.sprite = _splashSprite;
        }
    }
}