using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Gameplay
{
    public class HealthAnimation : MonoBehaviour
    {
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _increaseHealthAnimationDuration;
        [SerializeField] private float _increaseHealthPauseDuration;
        
        public void PlayHealthAnimation(Image image, float to)
        {
            DOTween
                .To(() => image.fillAmount, x => image.fillAmount = x, to, _animationDuration)
                .SetEase(Ease.Linear); 
        }
        
        public void IncreaseHealthAnimation(Image image)
        {
            image.transform.localScale = Vector3.zero;
            image.fillAmount = 1f;
            DOTween.Sequence()
                .AppendInterval(_increaseHealthPauseDuration)
                .Append(image.transform.DOScale(1f, _increaseHealthAnimationDuration))
                .Play();

        }
        
    }
}