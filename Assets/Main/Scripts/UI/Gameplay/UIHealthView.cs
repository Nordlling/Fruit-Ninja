using System;
using System.Collections;
using System.Collections.Generic;
using Main.Scripts.Infrastructure.Services.Health;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Gameplay
{
    public class UIHealthView : MonoBehaviour
    {
        [SerializeField] private GameObject _healthPanel;
        [SerializeField] private Image _healthImagePrefab;
        [SerializeField] private float _healthAnimationSpeed;

        private readonly List<Image> _currentHealthImages = new();
        private readonly List<Image> _allHealthImages = new();
        
        private IHealthService _healthService;
        
        private const float _epsilon = 0.1f;

        public void Construct(IHealthService healthService)
        {
            _healthService = healthService;
        }

        private void Start()
        {
            InitHealths();
        }

        private void InitHealths()
        {
            ClearAllChildren();
            for (int i = 0; i < _healthService.LeftHealths; i++)
            {
                Image healthImage = Instantiate(_healthImagePrefab, _healthPanel.transform);
                healthImage.fillAmount = 0f;
                StartCoroutine(StartHealthAnimation(healthImage,  1f));
                _currentHealthImages.Add(healthImage);
                _allHealthImages.Add(healthImage);
            }
        }
        
        private void ResetHealths()
        {
            foreach (Image healthImage in _allHealthImages)
            {
                StartCoroutine(StartHealthAnimation(healthImage,  1f));
                _currentHealthImages.Add(healthImage);
            }
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

        private void DecreaseHealth()
        {
            if (_currentHealthImages.Count > 0)
            {
                StartCoroutine(StartHealthAnimation(_currentHealthImages[0],  0f));
                _currentHealthImages.Remove(_currentHealthImages[0]);
            }
        }

        private IEnumerator StartHealthAnimation(Image image, float to)
        {
            while (Math.Abs(image.fillAmount - to) > _epsilon)
            {
                image.fillAmount = Mathf.Lerp(image.fillAmount, to, _healthAnimationSpeed * Time.deltaTime);
                yield return null;
            }
            image.fillAmount = to;
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