using UnityEngine;

namespace Main.Scripts.Logic.Blocks.Mimics
{
    public class Mimic : BlockPiece
    {
        public MimicSwitcher MimicSwitcher => _mimicSwitcher;
        
        [SerializeField] private MimicSwitcher _mimicSwitcher;

        private void OnDestroy()
        {
            _blockContainerService?.RemoveBlock(this);
        }
    }
}