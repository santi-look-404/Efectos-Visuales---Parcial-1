using System.Collections;
using UnityEngine;

public class ShieldShader : MonoBehaviour
{
    [Header("Key")]
    [SerializeField] string _disolvePropertyName = "_DissolveValue";
    [SerializeField] string _zoomObjectScreenPositionPropertyName = "_ZoomObjectScreenPosition";

    [Header("Disolve")]
    [SerializeField] float _disolveSpeed;

    bool _shieldOn;
    Camera _camera;
    Coroutine _disolveCoroutine;
    Renderer _renderer;

    void Start()
    {
        _camera = Camera.main;

        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        LookAtCamera();

        Zoom();

        ToggleShield();
    }

    private void LookAtCamera()
    {
        transform.forward = _camera.transform.position - transform.position;
    }

    private void Zoom()
    {
        Vector3 screenPoint = _camera.WorldToScreenPoint(transform.position);

        Vector3 zoom = new Vector3(screenPoint.x / Screen.width, screenPoint.y / Screen.height, screenPoint.z / Screen.width);

        _renderer.material.SetVector(_zoomObjectScreenPositionPropertyName, zoom);
    }

    private void ToggleShield()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateShield();
        }
    }

    private void ActivateShield()
    {
        float target = 1;

        if (_shieldOn)
        {
            target = 0;
        }

        _shieldOn = !_shieldOn;

        if (_disolveCoroutine != null)
        {
            StopCoroutine(_disolveCoroutine);
        }

        _disolveCoroutine = StartCoroutine(Coroutine_DisolveShield(target));
    }

    private IEnumerator Coroutine_DisolveShield(float target)
    {
        float start = _renderer.material.GetFloat(_disolvePropertyName);

        float lerp = 0;

        while (lerp < 1)
        {
            _renderer.material.SetFloat(_disolvePropertyName, Mathf.Lerp(start, target, lerp));

            lerp += Time.deltaTime * _disolveSpeed;

            yield return null;
        }
    }
}
