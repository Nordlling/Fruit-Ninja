using UnityEngine;

namespace Main.Scripts.Logic.Spawn
{
    public class SpawnVisualizer : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Camera _camera;

        private void OnDrawGizmos()
        {
            foreach (SpawnInfo spawnInfo in _spawner.SpawnAreas)
            {
                SetPosition(spawnInfo.spawnerAreaInfo);
                DrawSpawnField(spawnInfo);
                DrawAngles(spawnInfo);
            }
        }

        private void SetPosition(SpawnerAreaInfo spawnerInfo)
        {
            float currentScreenWidth = _camera.pixelWidth;
            float currentScreenHeight = _camera.pixelHeight;
            
            float firstPositionX = currentScreenWidth * spawnerInfo._firstPointXPercents;
            float firstPositionY = currentScreenHeight * spawnerInfo._firstPointYPercents;
            
            float lastPositionX = currentScreenWidth * spawnerInfo._lastPointXPercents;
            float lastPositionY = currentScreenHeight * spawnerInfo._lastPointYPercents;
            
            spawnerInfo._firstPoint = _camera.ScreenToWorldPoint(new Vector2(firstPositionX, firstPositionY));
            spawnerInfo._lastPoint = _camera.ScreenToWorldPoint(new Vector2(lastPositionX, lastPositionY));
        }

        private void DrawAngles(SpawnInfo spawnInfo)
        {
            Gizmos.color = Color.yellow;
            Vector2 firstPoint = spawnInfo.spawnerAreaInfo._firstPoint;
            Vector2 lastPoint = spawnInfo.spawnerAreaInfo._lastPoint;
            Vector2 lineVector = lastPoint - firstPoint;
            Vector2 centerPoint = Vector2.Lerp(firstPoint, lastPoint, 0.5f);
            Vector2 normal = new Vector2(-lineVector.y, lineVector.x).normalized;
            Vector2 rotatedLeftVector = Quaternion.Euler(0, 0, -(-spawnInfo.spawnerAreaInfo._leftAngle)) * normal;
            Vector2 rotatedRightVector = Quaternion.Euler(0, 0, -(spawnInfo.spawnerAreaInfo._rightAngle)) * normal;
            Gizmos.DrawRay(centerPoint, rotatedLeftVector.normalized);
            Gizmos.DrawRay(centerPoint, rotatedRightVector.normalized);
        }

        private void DrawSpawnField(SpawnInfo spawnInfo)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(spawnInfo.spawnerAreaInfo._firstPoint, spawnInfo.spawnerAreaInfo._lastPoint);
        }
    }
}