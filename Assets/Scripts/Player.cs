using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Control")]
    [SerializeField] private float _moveSpeed = 0.5f;
    [SerializeField] private float _xAxisSpeed = 2;
    [SerializeField] private float _yAxisSpeed = 2;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        transform.position += _moveSpeed * (Input.GetAxisRaw("Vertical") * transform.forward + Input.GetAxisRaw("Horizontal") * transform.right);

        transform.eulerAngles += new Vector3(-_yAxisSpeed * Input.GetAxis("Mouse Y"), _xAxisSpeed * Input.GetAxis("Mouse X"), 0.0f);
    }
}
