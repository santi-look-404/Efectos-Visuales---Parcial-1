using System.Collections;
using UnityEngine;

public class ShieldShader : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] string _disolvePropertyName = "_DissolveValue";
    [SerializeField] string _zoomObjectScreenPositionPropertyName = "_ZoomObjectScreenPosition";
    [SerializeField] string _vertexDisplacementPropertyName = "_VertexDisplacementStrength";
    [SerializeField] string _hitPositionPropertyName = "_HitPosition";

    [Header("Disolve")]
    [SerializeField] float _disolveSpeed;

    [Header("Hits")]
    [SerializeField] AnimationCurve _displacementCurve;
    [SerializeField] float _displacementMagnitude;
    [SerializeField] float _displacementLerpSpeed;

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

        CheckToggleShield();

        CheckClickOnShield();
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

    private void CheckToggleShield()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateShield();
        }
    }

    private void CheckClickOnShield()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                HitShield(hit.point);
            }
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

        _disolveCoroutine = StartCoroutine(DisolveShield(target));
    }

    public void HitShield(Vector3 hitPosition)
    {
        _renderer.material.SetVector(_hitPositionPropertyName, hitPosition);

        StopAllCoroutines();

        StartCoroutine(HitDisplacement());
    }

    private IEnumerator DisolveShield(float target)
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

    private IEnumerator HitDisplacement()
    {
        float lerp = 0;

        while (lerp < 1)
        {
            _renderer.material.SetFloat(_vertexDisplacementPropertyName, _displacementCurve.Evaluate(lerp) * _displacementMagnitude);
            
            lerp += Time.deltaTime * _displacementLerpSpeed;
            
            yield return null;
        }
    }
}
