using DG.Tweening;
using Main.Scripts.Infrastructure.Provides;
using UnityEngine;

namespace Main.Scripts.Logic.Splashing
{
    public class MagnetAreaEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _magnetAreaEffects;
        [SerializeField] private float _disappearDuration;
        [SerializeField] private Transform _effectTransform;
        
        private ITimeProvider _timeProvider;
        private float _radius;
        private float _lifeTime;
        
        private Sequence _sequence;

        public void Construct(ITimeProvider timeProvider, float radius, float lifeTime)
        {
            _timeProvider = timeProvider;
            _radius = radius;
            _lifeTime = lifeTime;
        }

        private void Start()
        {
            _effectTransform.localScale = new Vector3(_radius * 2f, _radius * 2f, _radius * 2f);
            PlayDisappearMagnetArea();
        }

        private void Update()
        {
            _sequence.timeScale = _timeProvider.GetTimeScale();
            
            for (int i = 0; i < _magnetAreaEffects.Length; i++)
            {
                var magnetAreaEffectMain = _magnetAreaEffects[i].main;
                magnetAreaEffectMain.simulationSpeed = _timeProvider.GetTimeScale();
            }
        }

        private void PlayDisappearMagnetArea()
        {
            _sequence = DOTween.Sequence()
                .AppendInterval(_lifeTime)
                .Append(_effectTransform.DOScale(0f, _disappearDuration).OnKill(() => Destroy(gameObject)))
                .Play();
        }
    }
}