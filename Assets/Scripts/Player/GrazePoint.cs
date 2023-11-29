using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrazePoint : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // generate EXP item
            Debug.Log("EXP");
        }
    }
}
