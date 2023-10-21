using System.Collections.Generic;
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

        private readonly List<Image> _currentHealthImages = new();
        private readonly List<Image> _allHealthImages = new();
        
        private IHealthService _healthService;

        public void Construct(IHealthService healthService)
        {
            _healthService = healthService;
        }

        private void Start()
        {
            InitHealths();
        }

        private void OnEnable()
        {
            _healthService.OnDamaged += DecreaseHealth;
            _healthService.OnReset += ResetHealths;
        }

        private void OnDisable()
        {
            _healthService.OnDamaged -= DecreaseHealth;
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
                _currentHealthImages.Add(healthImage);
                _allHealthImages.Add(healthImage);
            }
        }
        
        private void ResetHealths()
        {
            foreach (Image healthImage in _allHealthImages)
            {
                _healthAnimation.PlayHealthAnimation(healthImage,  1f);
                _currentHealthImages.Add(healthImage);
            }
        }

        private void DecreaseHealth()
        {
            if (_currentHealthImages.Count > 0)
            {
                _healthAnimation.PlayHealthAnimation(_currentHealthImages[0],  0f);
                _currentHealthImages.Remove(_currentHealthImages[0]);
            }
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