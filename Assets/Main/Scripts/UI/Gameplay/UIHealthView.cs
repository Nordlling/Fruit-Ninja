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
        [Header("Health")]
        [SerializeField] private GameObject _healthPanel;
        [SerializeField] private Image _healthImagePrefab;
        [SerializeField] private float _healthAnimationSpeed;

        private readonly List<Image> _healthImages = new();
        
        private IHealthService _healthService;

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
            for (int i = 0; i < _healthService.LeftHealths; i++)
            {
                Image healthImage = Instantiate(_healthImagePrefab, _healthPanel.transform);
                healthImage.fillAmount = 0f;
                StartCoroutine(StartHealthAnimation(healthImage,  1f));
                _healthImages.Add(healthImage);
            }
        }

        private void OnEnable()
        {
            _healthService.OnDamaged += DecreaseHealth;
        }

        private void OnDisable()
        {
            _healthService.OnDamaged -= DecreaseHealth;
        }

        private void DecreaseHealth()
        {
            if (_healthImages.Count > 0)
            {
                StartCoroutine(StartHealthAnimation(_healthImages[0],  0f));
                _healthImages.Remove(_healthImages[0]);
            }
        }

        private IEnumerator StartHealthAnimation(Image image, float to)
        {
            while (Math.Abs(image.fillAmount - to) > float.Epsilon)
            {
                image.fillAmount = Mathf.Lerp(image.fillAmount, to, _healthAnimationSpeed * Time.deltaTime);
                yield return null;
            }
            image.fillAmount = to;
        }
        
    }
}