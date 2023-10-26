using DG.Tweening;
using Main.Scripts.Infrastructure.Provides;
using UnityEngine;

namespace Main.Scripts.Logic.Splashing
{
    public class MagnetAreaEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _magnetAreaEffect;
        [SerializeField] private float _disappearDuration;
        [SerializeField] private Transform _effectTransform;
        
        private ITimeProvider _timeProvider;
        private float _radius;
        private float _lifeTime;
        
        private Sequence _sequence;
        private ParticleSystem.MainModule _magnetAreaEffectMain;

        public void Construct(ITimeProvider timeProvider, float radius, float lifeTime)
        {
            _timeProvider = timeProvider;
            _radius = radius;
            _lifeTime = lifeTime;
        }

        private void Start()
        {
            _effectTransform.localScale = new Vector3(_radius * 2f, _radius * 2f, _radius * 2f);
            _magnetAreaEffectMain = _magnetAreaEffect.main;
            PlayDisappearMagnetArea();
        }

        private void Update()
        {
            _sequence.timeScale = _timeProvider.GetTimeScale();
            _magnetAreaEffectMain.simulationSpeed = _timeProvider.GetTimeScale();
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