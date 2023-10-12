using Main.Scripts.Infrastructure.Factory;
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
        
        private IGameFactory _gameFactory;
        private Sprite _splashSprite;

        public void Construct(IGameFactory gameFactory, Sprite splashSprite)
        {
            _gameFactory = gameFactory;
            _splashSprite = splashSprite;
        }
        public void Slice(Vector2 swipePosition)
        {
            Sprite originalSprite = _spriteRenderer.sprite;
            
            SlicedRect slicedRect = CreateSlicedRect(swipePosition, originalSprite);

            CreateSpritePart(originalSprite, slicedRect.FirstRectPart, slicedRect.FirstRectPartDirection);
            CreateSpritePart(originalSprite, slicedRect.SecondRectPart, slicedRect.SecondRectPartDirection);

            SpawnSplash();

            Destroy(gameObject);
        }

        private SlicedRect CreateSlicedRect(Vector2 swipePosition, Sprite originalSprite)
        {
            Rect rect = new Rect(0f, 0f, originalSprite.texture.width, originalSprite.texture.height);
            Vector2 worldDirection = ((Vector2)transform.position - swipePosition);
            Vector2 rectDirection = worldDirection *
                                    new Vector2(rect.width / transform.localScale.x, rect.height / transform.localScale.y);
            Vector2 rectPoint = rect.center - rectDirection;
            SlicedRect slicedRect = rect.GetSlicedRect(rectPoint, _rectPieceDirectionAngle);
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