using System;
using Main.Scripts.Infrastructure.Services.Samuraism;
using TMPro;
using UnityEngine;

namespace Main.Scripts.UI.Gameplay
{
    public class UISamuraiView : MonoBehaviour
    {
        [SerializeField] private SamuraiTimerAnimation _samuraiTimerAnimation;
        [SerializeField] private TextMeshProUGUI _timerValue;
        
        private ISamuraiService _samuraiService;

        public void Construct(ISamuraiService samuraiService)
        {
            _samuraiService = samuraiService;
        }

        private void OnEnable()
        {
            _samuraiService.OnStarted += StartSamuraiView;
            _samuraiService.OnTimerUpdated += UpdateTimer;
            _samuraiService.OnFinished += FinishSamuraiView;
        }

        private void OnDisable()
        {
            _samuraiService.OnStarted -= StartSamuraiView;
            _samuraiService.OnTimerUpdated -= UpdateTimer;
            _samuraiService.OnFinished -= FinishSamuraiView;
        }

        private void UpdateTimer(float time)
        {
            _timerValue.text = Math.Ceiling(time).ToString();
        }
        
        private void StartSamuraiView()
        {
            _samuraiTimerAnimation.PlayFadeInAnimation();
        }
        
        private void FinishSamuraiView()
        {
            _samuraiTimerAnimation.PlayFadeOutAnimation();
        }
        
    }
}