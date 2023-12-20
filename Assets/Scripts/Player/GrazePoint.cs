using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GrazePoint : MonoBehaviour
{
    [SerializeField] Exp _expPrefab;

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

        Exp exp = _expPrefab.GetPooledObject().GetComponent<Exp>();
        exp.transform.position = position;

        exp.Init(RandomAmountGenerator());
    }

    private int RandomAmountGenerator()
    {
        int amount;
        float x = Random.Range(0f, 1f);

        if (x <= 0.1)
            amount = 2;
        else if (x <= 0.3)
            amount = 1;
        else
            amount = 0;

        return amount;
    }
}
