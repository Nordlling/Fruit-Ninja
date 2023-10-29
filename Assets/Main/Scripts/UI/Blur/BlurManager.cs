using Main.Scripts.Constants;
using Main.Scripts.Utils.MaterialUtils;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.Blur
{
    public class BlurManager : MonoBehaviour
    {
        [SerializeField] private Image _originalImage;
        [SerializeField] private Image _image;
        [SerializeField] private Material _material;

        [Range(0, 64)]
        [SerializeField] private int _blurRadius;
        [Range(3, 10)]
        [SerializeField] private int _stepSize;
        [Range(0.0001f, 0.3f)]
        [SerializeField] private float _jumpSize;

        private void Start()
        {
            Material newMaterial = new Material(_material);
            newMaterial.SetInt(BlurMaterialParams.Radius, _blurRadius);
            newMaterial.SetInt(BlurMaterialParams.Step, _stepSize);
            newMaterial.SetFloat(BlurMaterialParams.Jump, _jumpSize);
            newMaterial.SetTexture(BlurMaterialParams.BlurTex, _originalImage.sprite.texture);
            _image.sprite = newMaterial.CreateRenderSprite(_originalImage.sprite.texture.width, _originalImage.sprite.texture.height);
        }
    }
}