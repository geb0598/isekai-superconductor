using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, _rotationSpeed * Time.fixedDeltaTime);        
    }
}
