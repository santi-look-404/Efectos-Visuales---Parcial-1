using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWithShield : MonoBehaviour
{
    [SerializeField] ShieldShader _shield;
    [SerializeField] float _distanceRadius = 10f;
    [SerializeField] float _checkTime = 3f;

    FPSPlayer _fpsPlayer;

    void Start()
    {
        _fpsPlayer = FindObjectOfType<FPSPlayer>();

        StartCoroutine(CheckPlayer());
    }

    void Update()
    {
        
    }

    IEnumerator CheckPlayer()
    {
        while (true)
        {
            float distanceFromPlayer = Vector3.Distance(transform.position, _fpsPlayer.transform.position);

            if (distanceFromPlayer < _distanceRadius)
            {
                if (!_shield._shieldOn)
                {
                    _shield.ActivateShield();
                }
            }
            else
            {
                if (_shield._shieldOn)
                {
                    _shield.ActivateShield();
                }
            }

            yield return new WaitForSeconds(_checkTime);
        }
    }
}
