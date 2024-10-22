using UnityEngine;

public class EagleVisionManager : MonoBehaviour
{
    [SerializeField] private Material _backgroundColorEffect;

    public static EagleVisionManager Instance;

    bool _isOn = false;

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
        SetEagleVisionIsOn(false);
    }

    private void OnApplicationQuit()
    {
        _backgroundColorEffect.RevertAllPropertyOverrides();
    }

    public void ToggleEagleVision()
    {
        _isOn = !_isOn;

        SetEagleVisionIsOn(_isOn);
    }

    private void SetEagleVisionIsOn(bool isOn)
    {
        _backgroundColorEffect.SetFloat("_IsOn", isOn ? 1 : 0);
    }
}
