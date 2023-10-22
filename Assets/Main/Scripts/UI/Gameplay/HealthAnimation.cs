using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Gameplay
{
    public class HealthAnimation : MonoBehaviour
    {
        [SerializeField] private float _healthAnimationSpeed;
        
        public void PlayHealthAnimation(Image image, float to)
        {
            DOTween
                .To(() => image.fillAmount, x => image.fillAmount = x, to, _healthAnimationSpeed)
                .SetEase(Ease.Linear); 
        }
        
    }
}