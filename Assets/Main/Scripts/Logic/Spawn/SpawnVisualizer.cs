using UnityEngine;

namespace Main.Scripts.Logic.Spawn
{
    public class SpawnVisualizer : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Camera _camera;
        
        private Resolution _currentResolution;

        private void OnDrawGizmos()
        {
            foreach (SpawnInfo spawnInfo in _spawner.SpawnAreas)
            {
                SetPosition(spawnInfo.spawnerAreaInfo);
                DrawSpawnField(spawnInfo.spawnerAreaInfo);
                DrawAngles(spawnInfo.spawnerAreaInfo);
            }
        }

        private void Start()
        {
            _currentResolution = Screen.currentResolution;
            UpdatePosition();
        }

        private void Update()
        {
            if (ResolutionIsChanged())
            {
                UpdatePosition();
            }
        }

        private bool ResolutionIsChanged()
        {
            return Screen.currentResolution.width != _currentResolution.width ||
                   Screen.currentResolution.height != _currentResolution.height;
        }

        private void UpdatePosition()
        {
            foreach (SpawnInfo spawnInfo in _spawner.SpawnAreas)
            {
                SetPosition(spawnInfo.spawnerAreaInfo);
            }
        }

        private void SetPosition(SpawnerAreaInfo spawnerInfo)
        {
            float currentScreenWidth = _camera.pixelWidth;
            float currentScreenHeight = _camera.pixelHeight;
            
            float firstPositionX = currentScreenWidth * spawnerInfo._firstPointX;
            float firstPositionY = currentScreenHeight * spawnerInfo._firstPointY;
            
            float lastPositionX = currentScreenWidth * spawnerInfo._lastPointX;
            float lastPositionY = currentScreenHeight * spawnerInfo._lastPointY;
            
            spawnerInfo._firstPoint = _camera.ScreenToWorldPoint(new Vector2(firstPositionX, firstPositionY));
            spawnerInfo._lastPoint = _camera.ScreenToWorldPoint(new Vector2(lastPositionX, lastPositionY));
        }

        private void DrawAngles(SpawnerAreaInfo spawnInfo)
        {
            Gizmos.color = Color.yellow;
            Vector2 firstPoint = spawnInfo._firstPoint;
            Vector2 lastPoint = spawnInfo._lastPoint;
            Vector2 lineVector = lastPoint - firstPoint;
            Vector2 centerPoint = Vector2.Lerp(firstPoint, lastPoint, 0.5f);
            Vector2 normal = new Vector2(-lineVector.y, lineVector.x).normalized;
            Vector2 rotatedLeftVector = Quaternion.Euler(0, 0, -(-spawnInfo._leftAngle)) * normal;
            Vector2 rotatedRightVector = Quaternion.Euler(0, 0, -(spawnInfo._rightAngle)) * normal;
            Gizmos.DrawRay(centerPoint, rotatedLeftVector.normalized);
            Gizmos.DrawRay(centerPoint, rotatedRightVector.normalized);
        }

        private void DrawSpawnField(SpawnerAreaInfo spawnInfo)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(spawnInfo._firstPoint, spawnInfo._lastPoint);
        }
    }
}