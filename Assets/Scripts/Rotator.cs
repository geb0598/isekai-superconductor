using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] int _rotationCount;
    [SerializeField] float _rotationSpeed;

    private int _cumulativeRotation = 0;
    private float _cumulativeAngle = 0;

    private void FixedUpdate()
    {
        _cumulativeAngle += _rotationSpeed * Time.fixedDeltaTime;
        if (_cumulativeAngle >= 360)
        {
            ++_cumulativeRotation;
            _cumulativeAngle -= 360;
        }
        if (_rotationCount == 0 || _rotationCount > _cumulativeRotation)
        {
            transform.Rotate(0, 0, _rotationSpeed * Time.fixedDeltaTime);        
        }
    }
}
