using UnityEngine;

namespace Main.Scripts.Logic.Blocks
{
    public class BlockCollider : MonoBehaviour
    {
        [SerializeField] private float _sphereScale;
        
        public Bounds SphereBounds { get; private set; }
        private Vector3 _sphereCenter;
        private float _sphereRadius;
        private void Start()
        {
            _sphereCenter = transform.position;
            _sphereRadius = transform.localScale.x * _sphereScale;
            SphereBounds = new Bounds(_sphereCenter, new Vector3(_sphereRadius, _sphereRadius, _sphereRadius));  
        }

        private void Update()
        {
            UpdateBounds();
        }

        private void UpdateBounds()
        {
            var sphereBounds = SphereBounds;
            sphereBounds.center = transform.position;
            SphereBounds = sphereBounds;
        }

        private void OnDrawGizmos()
        {
            _sphereCenter = transform.position;
            _sphereRadius = transform.localScale.x * _sphereScale;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_sphereCenter, _sphereRadius / 2f);
        }
    }
}