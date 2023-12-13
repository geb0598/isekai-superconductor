using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRelocator : MonoBehaviour
{
    [SerializeField] float _width;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("GroundDetector"))
        {
            return;
        }

        Vector3 direction = PlayerManager.instance.player.transform.position - transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            direction.y = 0;
        } 
        else
        {
            direction.x = 0;
        }

        transform.Translate(direction.normalized * _width);
    }
}
