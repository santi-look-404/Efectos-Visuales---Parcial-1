using System.Collections;
using UnityEngine;

public class EagleVisionManager : MonoBehaviour
{
    [Header("Background")]
    [SerializeField] private string _backgroundColorEffectIsOnName = "_IsOn";
    [SerializeField] private string _backgroundColorEffectBlurMaskSizeName = "_BlurMaskSize";
    [SerializeField] private Material _backgroundColorEffect;

    [Header("Outlines")]
    [SerializeField] private string _borderColorEffectMinDepthDistanceName = "_MinDepthDistance";
    [SerializeField] private Material _borderColorEffect;

    [Header("Transition Speed")]
    [SerializeField] private float _transitionSpeed = 3.0f;

    public static EagleVisionManager Instance;

    bool _isOn = false;
    Coroutine _transitionCoroutine = null;
    float _transition = 0;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeEagleVisionRate();
    }

    /*
    private void OnApplicationQuit()
    {
        Material[] materials = new Material[] { _backgroundColorEffect, _borderColorEffect };
        
        if (!Application.isEditor) return;

        foreach (Material material in materials)
        {
            material.RevertAllPropertyOverrides();
        }
    }
    */

    public void ToggleEagleVision()
    {
        _isOn = !_isOn;

        _transitionCoroutine ??= StartCoroutine(ToggleBackground());
    }

    private IEnumerator ToggleBackground()
    {
        while (_transition >= 0 && _transition <= 1)
        {
            _transition = Mathf.Clamp(_transition + (_isOn ? 1 : -1) * Time.deltaTime * _transitionSpeed, 0, 1);

            ChangeEagleVisionRate();

            yield return null;
        }

        _transitionCoroutine = null;
    }

    private void ChangeEagleVisionRate()
    {
        _backgroundColorEffect.SetFloat(_backgroundColorEffectIsOnName, _transition);

        _backgroundColorEffect.SetFloat(_backgroundColorEffectBlurMaskSizeName, _transition);

        _borderColorEffect.SetFloat(_borderColorEffectMinDepthDistanceName, _transition);

        GhostManager.Instance.ToggleGhosts(_transition);
    }
}
