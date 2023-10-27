using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Mimics
{
    public class Mimic : BlockPiece
    {
        public MimicSwitcher MimicSwitcher => _mimicSwitcher;
        
        [SerializeField] private MimicSwitcher _mimicSwitcher;
        
        public void Slice(Vector2 swiperPosition, Vector2 swiperDirection)
        {
            
        }

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBlock(this);
        }
    }
}