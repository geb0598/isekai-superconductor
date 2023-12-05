using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrazePoint : MonoBehaviour
{
    [SerializeField] float _minExpGenerationRange;
    [SerializeField] float _maxExpGenerationRange;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GenerateExpItem();
        }
    }

    private void GenerateExpItem()
    {
        float length = Random.Range(_minExpGenerationRange, _maxExpGenerationRange);
        Vector2 direction = Random.insideUnitCircle.normalized;
        Vector2 position = (Vector2)transform.position + length * direction;

        Exp exp = GameManager.GetInstance().poolManager.GetComponent<Exp>();
        exp.transform.position = position;
        exp.Init(1);
    }
}
