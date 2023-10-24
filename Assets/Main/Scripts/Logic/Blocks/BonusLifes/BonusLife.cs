using UnityEngine;

namespace Main.Scripts.Logic.Blocks.BonusLifes
{
    public class BonusLife : BlockPiece, ISliceable
    {
        public BonusLifeSlicer BonusLifeSlicer => _bonusLifeSlicer;
        public float SpeedMultiplier => _speedMultiplier;
        
        [SerializeField] private BonusLifeSlicer _bonusLifeSlicer;
        [SerializeField] private float _speedMultiplier;
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            _bonusLifeSlicer.Slice(swiperPosition, swiperDirection);
        }

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBlock(this);
        }
    }
}