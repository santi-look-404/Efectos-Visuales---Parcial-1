using UnityEngine;

public class ShieldShader : MonoBehaviour
{
    [Header("Zoom")]
    [SerializeField] string _zoomObjectScreenPositionPropertyName = "_ZoomObjectScreenPosition";

    Camera _camera;
    Renderer _renderer;

    void Start()
    {
        _camera = Camera.main;

        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        LookAtCamera();
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
}
