using System.Collections.Generic;
using Main.Scripts.Infrastructure.Services.AnimationTargetContainer;
using Main.Scripts.Infrastructure.Services.Health;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Gameplay
{
    public class UIHealthView : MonoBehaviour
    {
        [SerializeField] private HealthAnimation _healthAnimation;
        [SerializeField] private GameObject _healthPanel;
        [SerializeField] private Image _healthImagePrefab;

        private int _currentHealthCount;
        private readonly List<Image> _allHealthImages = new();
        
        private IHealthService _healthService;
        private IAnimationTargetContainer _animationTargetContainer;

        public void Construct(IHealthService healthService, IAnimationTargetContainer animationTargetContainer)
        {
            _healthService = healthService;
            _animationTargetContainer = animationTargetContainer;
        }

        private void Start()
        {
            InitHealths();
        }

        private void OnEnable()
        {
            _healthService.OnDecreased += DecreaseHealth;
            _healthService.OnIncreased += IncreaseHealth;
            _healthService.OnReset += ResetHealths;
        }

        private void OnDisable()
        {
            _healthService.OnDecreased -= DecreaseHealth;
            _healthService.OnIncreased -= IncreaseHealth;
            _healthService.OnReset -= ResetHealths;
        }

        private void InitHealths()
        {
            ClearAllChildren();
            for (int i = 0; i < _healthService.LeftHealths; i++)
            {
                Image healthImage = Instantiate(_healthImagePrefab, _healthPanel.transform);
                healthImage.fillAmount = 0f;
                _healthAnimation.PlayHealthAnimation(healthImage,  1f);
                _currentHealthCount++;
                _allHealthImages.Add(healthImage);
            }
        }
        
        private void ResetHealths()
        {
            _currentHealthCount = _allHealthImages.Count;
            
            foreach (Image healthImage in _allHealthImages)
            {
                _healthAnimation.PlayHealthAnimation(healthImage,  1f);
            }
        }

        private void DecreaseHealth()
        {
            if (_currentHealthCount > 0)
            {
                _currentHealthCount--;
                _healthAnimation.PlayHealthAnimation(_allHealthImages[_currentHealthCount],  0f);
            }
        }

        private void IncreaseHealth()
        {
            _animationTargetContainer.HealthTarget = _allHealthImages[_currentHealthCount].transform.position;
            _healthAnimation.IncreaseHealthAnimation(_allHealthImages[_currentHealthCount]);
            _currentHealthCount++;
        }

        private void ClearAllChildren()
        {
            int childCount = _healthPanel.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = _healthPanel.transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }
    }
}