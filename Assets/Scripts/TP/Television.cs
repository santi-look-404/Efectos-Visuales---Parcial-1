using UnityEngine;

public class Television : MonoBehaviour, IInteractable
{
    bool _isOn = false;
    Material _tvMaterial;

    private void Start()
    {
        _tvMaterial = GetComponent<Renderer>().material;
    }

    public void ExecuteInteraction()
    {
        _isOn = !_isOn;

        _tvMaterial.SetFloat("_IsOn", _isOn ? 1 : 0);
    }
}
