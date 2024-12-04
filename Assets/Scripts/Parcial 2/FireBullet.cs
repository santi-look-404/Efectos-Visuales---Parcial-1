using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float _speed;
    [SerializeField] GameObject _HitEffect;

    [Header("Materials")]
    [SerializeField] Renderer _sphereMaterial;

    [Header("Aesthetics")]
    [SerializeField][ColorUsage(true, true)] Color[] _possibleColors;

    Ray ray;
    RaycastHit hit;
    Vector3 _hitNormal;
    Vector3 _hitPos;

    private void Start()
    {
        _sphereMaterial.material.SetColor("_Color", _possibleColors[Random.Range(0, _possibleColors.Length)]);

        Destroy(gameObject, 3);
    }

    void Update()
    {
        Vector3 delta = transform.forward * _speed * Time.deltaTime;

        transform.position += delta;

        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            _hitNormal = hit.normal;

            _hitPos = hit.point;

            float distance = (transform.position - hit.point).magnitude;

            if (distance < delta.magnitude)
            {
                Hit(hit.collider);
            }
        }
    }

    void Hit(Collider collider)
    {
        GameObject hit = Instantiate(_HitEffect, _hitPos, Quaternion.identity);

        ShieldShader shield = collider.GetComponentInParent<ShieldShader>();

        if (shield != null)
        {
            shield.HitShield(_hitPos);
        }

        hit.transform.forward = _hitNormal;

        Destroy(gameObject);
    }

    public void Configure(FPSPlayer fpsPlayer)
    {
        transform.forward = fpsPlayer.transform.forward;

        transform.SetParent(null);
    }
}
