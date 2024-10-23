using UnityEngine;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    [Header("Floating Motion")]
    [SerializeField] private float _frequency = 1;
    [SerializeField] private float _amplitude = 1;

    [Header("Material")]
    [SerializeField] Material _material;

    void Update()
    {
        transform.position += _amplitude * Mathf.Sin(_frequency * Time.timeSinceLevelLoad) * Vector3.up;
    }

    public void Toggle(float transition)
    {
        _material.SetFloat("_Alpha", transition);
    }
}
