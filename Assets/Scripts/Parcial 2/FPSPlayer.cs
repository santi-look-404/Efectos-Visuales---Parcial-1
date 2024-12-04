using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    [Header("Control")]
    [SerializeField] private float _moveSpeed = 0.5f;
    [SerializeField] private float _xAxisSpeed = 2;
    [SerializeField] private float _yAxisSpeed = 2;

    [Header("Bullets")]
    [SerializeField] private FireBullet _fireBulletPrefab;

    float _base;

    private void Start()
    {
        Cursor.visible = false;

        _base = transform.position.y;
    }

    void Update()
    {
        Move(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));

        RotateCamera(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        Shoot();

        Quit();
    }

    void Move(float verticalAxis, float horizontalAxis)
    {
        transform.position += _moveSpeed * (verticalAxis * transform.forward + horizontalAxis * transform.right);

        transform.position = new Vector3(transform.position.x, _base, transform.position.z);
    }

    void RotateCamera(float yAxis, float xAxis)
    {
        transform.eulerAngles += new Vector3(-_yAxisSpeed * yAxis, _xAxisSpeed * xAxis, 0.0f);
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet fireBullet = Instantiate(_fireBulletPrefab, transform);

            fireBullet.Configure(this);
        }
    }

    void Quit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
