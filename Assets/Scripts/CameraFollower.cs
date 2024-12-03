using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    void Update()
    {
        transform.position = _target.position;

        transform.rotation = _target.rotation;
    }
}
