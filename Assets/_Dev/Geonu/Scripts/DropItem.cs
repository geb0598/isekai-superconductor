using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Get()
    {
        Coin coin = GetComponent<Coin>();
        Exp exp = GetComponent<Exp>();

        if (coin == null)
        {
            int amount = exp.amount;
            // playerManager GetExp(amount) function call
        }

        else
        {
            int amount = coin.amount;
            // playerManager GetCoin(amount) function call
        }
    }

    // HitBox Tag Collider  
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            Get();
            gameObject.SetActive(false);
        }
    }

    // ItemGetter Tag Collider
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ItemGetter"))
        {
            // Move to Player
            Vector2 dirVec = (collision.transform.position - transform.position).normalized;
            _rigidbody2D.MovePosition(_rigidbody2D.position + dirVec * 0.1f);
        }
    }
}
