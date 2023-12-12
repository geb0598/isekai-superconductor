using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrazePoint : MonoBehaviour
{
    [SerializeField] float _minExpGenerationRange;
    [SerializeField] float _maxExpGenerationRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            GenerateExpItem();
        }
    }

    private void GenerateExpItem()
    {
        float length = Random.Range(_minExpGenerationRange, _maxExpGenerationRange);
        Vector2 direction = Random.insideUnitCircle.normalized;
        Vector2 position = (Vector2)transform.position + length * direction;

        Exp exp = GameManager.GetInstance().poolManager.Get(2, 2).GetComponent<Exp>();
        exp.transform.position = position;
        exp.Init(1);
    }
}
