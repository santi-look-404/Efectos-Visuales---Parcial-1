using UnityEngine;

public class ShieldShader : MonoBehaviour
{
    void Update()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        transform.forward = Camera.main.transform.position - transform.position;
    }
}
