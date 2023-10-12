using Main.Scripts.Infrastructure.Factory;
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
        [SerializeField] private Splash.Splash _splashPrefab;
        
        private IGameFactory _gameFactory;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        public void Slice(Vector2 swipePosition)
        {
            Sprite originalSprite = _spriteRenderer.sprite;
            
            Rect rect = new Rect(0f, 0f, originalSprite.texture.width, originalSprite.texture.height);
            Vector2 worldDirection = ((Vector2)transform.position - swipePosition);
            Vector2 rectDirection = worldDirection * new Vector2(rect.width / transform.localScale.x, rect.height / transform.localScale.y);
            Vector2 rectPoint = rect.center - rectDirection;
            SlicedRect slicedRect = rect.GetSlicedRect(rectPoint, _rectPieceDirectionAngle);
            
            Sprite leftSprite = Sprite.Create(
                originalSprite.texture, 
                slicedRect.FirstRectPart, 
                new Vector2(0.5f, 0.5f));
            
            BlockPiece firstBlockPiece = _gameFactory.CreateBlockPiece(_blockPiecePrefab, transform.position);
            firstBlockPiece.transform.rotation = transform.rotation;
            firstBlockPiece.BlockMovement.Construct(slicedRect.FirstRectPartDirection, 5f);
            firstBlockPiece.SpriteRenderer.sprite = leftSprite;
            
            
            Sprite rightSprite = Sprite.Create(
                originalSprite.texture, 
                slicedRect.SecondRectPart, 
                new Vector2(0.5f, 0.5f));
            
            BlockPiece secondBlockPiece =  _gameFactory.CreateBlockPiece(_blockPiecePrefab, transform.position);
            secondBlockPiece.transform.rotation = transform.rotation;
            secondBlockPiece.BlockMovement.Construct(slicedRect.SecondRectPartDirection, _departureSpeed);
            secondBlockPiece.SpriteRenderer.sprite = rightSprite;


            Instantiate(_splashPrefab, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}