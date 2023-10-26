using Main.Scripts.Infrastructure.Factory;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Logic.Blocks;
using Main.Scripts.Logic.Blocks.BlockBag;
using Main.Scripts.Logic.Blocks.Bombs;
using Main.Scripts.Logic.Blocks.BonusLifes;
using Main.Scripts.Logic.Blocks.Bricks;
using Main.Scripts.Logic.Blocks.Freezes;
using Main.Scripts.Logic.Blocks.Magnets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.Scripts.Logic.Spawn
{
    public class SpawnArea : MonoBehaviour
    {
        private readonly float _distanceToOffset = 1f;
        
        private SpawnerAreaInfo _spawnerInfo;

        private Vector2 _normal;
        
        private Vector2 _newPointPosition;
        private Vector2 _newStartDirection;
        private float _newSpeed;

        private IBlockFactory _blockFactory;
        private ITimeProvider _timeProvider;

        public void Construct(SpawnerAreaInfo spawnerInfo, IBlockFactory blockFactory)
        {
            _blockFactory = blockFactory;
            _spawnerInfo = spawnerInfo;

            CalculateNormal();
        }

        public void SpawnBlock()
        {
            GenerateBlockParameters();
            
            Block block = _blockFactory.CreateBlock(_newPointPosition, 0f);
            block.BlockMovement.Construct(_newStartDirection, _newSpeed, block.TimeProvider);
        }
        
        public void SpawnBomb()
        {
            GenerateBlockParameters();
            
            Bomb bomb = _blockFactory.CreateBomb(_newPointPosition);
            bomb.BlockMovement.Construct(_newStartDirection, _newSpeed, bomb.TimeProvider);
        }

        public void SpawnBonusLife()
        {
            GenerateBlockParameters();
            
            BonusLife bonusLife = _blockFactory.CreateBonusLife(_newPointPosition);
            bonusLife.BlockMovement.Construct(_newStartDirection, _newSpeed * bonusLife.SpeedMultiplier, bonusLife.TimeProvider);
        }
        
        public void SpawnBlockBag()
        {
            GenerateBlockParameters();
            
            BlockBag blockBag = _blockFactory.CreateBlockBag(_newPointPosition);
            blockBag.BlockMovement.Construct(_newStartDirection, _newSpeed, blockBag.TimeProvider);
        }
        
        public void SpawnFreeze()
        {
            GenerateBlockParameters();
            
            Freeze freeze = _blockFactory.CreateFreeze(_newPointPosition);
            freeze.BlockMovement.Construct(_newStartDirection, _newSpeed, freeze.TimeProvider);
        }

        public void SpawnMagnet()
        {
            GenerateBlockParameters();
            
            Magnet magnet = _blockFactory.CreateMagnet(_newPointPosition);
            magnet.BlockMovement.Construct(_newStartDirection, _newSpeed, magnet.TimeProvider);
        }

        public void SpawnBrick()
        {
            GenerateBlockParameters();
            
            Brick brick = _blockFactory.CreateBrick(_newPointPosition);
            brick.BlockMovement.Construct(_newStartDirection, _newSpeed, brick.TimeProvider);
        }

        private void GenerateBlockParameters()
        {
            _newStartDirection = GenerateDirection();
            OffsetPoint(_newStartDirection);
            _newSpeed = Random.Range(_spawnerInfo._minSpeed, _spawnerInfo._maxSpeed);
        }

        private void OffsetPoint(Vector2 direction)
        {
            _newPointPosition -= direction * _distanceToOffset;
        }

        private void CalculateNormal()
        {
            Vector2 lineVector = _spawnerInfo._lastPoint - _spawnerInfo._firstPoint;
            _normal = new Vector2(-lineVector.y, lineVector.x).normalized;
        }

        private Vector2 GenerateDirection()
        {
            GeneratePointPosition();
            Vector2 rotatedVector = GenerateAngle(_normal);
            return rotatedVector.normalized;
        }

        private void GeneratePointPosition()
        {
            float newPointValue = Random.Range(0f, 1f);
            _newPointPosition = Vector2.Lerp( _spawnerInfo._firstPoint, _spawnerInfo._lastPoint, newPointValue);
        }

        private Vector2 GenerateAngle(Vector2 normal)
        {
            float angleDegrees = Random.Range(-_spawnerInfo._leftAngle, _spawnerInfo._rightAngle);
            Vector2 rotatedVector = Quaternion.Euler(0, 0, -angleDegrees) * normal;
            return rotatedVector;
        }
    }
}
