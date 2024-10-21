using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour
{
    [Header("Control")]
    [SerializeField] private float _moveSpeed = 0.5f;
    [SerializeField] private float _xAxisSpeed = 2;
    [SerializeField] private float _yAxisSpeed = 2;

    [Header("Interaction")]
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private float _interactionMaxDistance = 2;

    RaycastHit _interactionHitInfo;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Move(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));

        RotateCamera(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        if (CanInteract() && Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            EagleVisionManager.Instance.ToggleEagleVision();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Move(float verticalAxis, float horizontalAxis)
    {
        transform.position += _moveSpeed * (verticalAxis * transform.forward + horizontalAxis * transform.right);
    }

    void RotateCamera(float yAxis, float xAxis)
    {
        transform.eulerAngles += new Vector3(-_yAxisSpeed * yAxis, _xAxisSpeed * xAxis, 0.0f);
    }

    bool CanInteract()
    {
        return Physics.Raycast(transform.position, transform.forward, out _interactionHitInfo, _interactionMaxDistance, _interactionLayer);
    }

    void Interact()
    {
        IInteractable interactable;

        if (_interactionHitInfo.collider.TryGetComponent(out interactable))
        {
            interactable.ExecuteInteraction();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, _interactionMaxDistance * transform.forward);
    }
}
