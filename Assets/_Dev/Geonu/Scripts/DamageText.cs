using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : PooledObject
{
    public TextMeshPro text;

    private float _timer;
    private float _lifeTime;
    private float _alphaSpeed;
    private Color alpha;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        _lifeTime = 1f;
        _alphaSpeed = 1f;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _lifeTime)
        {
            gameObject.SetActive(false);
        }

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * _alphaSpeed);
        text.color = alpha;
    }

    public void Init(float damage, Transform _transform)
    {
        _timer = 0;
        text.text = ((int)damage).ToString();
        alpha.a = 1;
        Vector3 playerPosition = GameManager.GetInstance().playerController.transform.position;
        transform.position = _transform.position + (playerPosition - _transform.position).normalized * 0.5f;
    }
}
