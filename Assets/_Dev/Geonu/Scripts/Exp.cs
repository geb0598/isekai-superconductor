using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : DropItem
{
    public Sprite[] sprites;
    [SerializeField] private int[] _amounts;

    private SpriteRenderer _spriteRenderer;
    private int _index;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int index)
    {
        _index = index;
        _spriteRenderer.sprite = sprites[index];

        if (index == 2)
        {
            _spriteRenderer.color = Color.red;
        }
    }

    public override void Get()
    {
        for (int i = 0; i < _amounts[_index]; i++)
        {
            PlayerManager.instance.AddExperiencePoints();
        }
        
        gameObject.SetActive(false);
        GameManager.GetInstance().eventManager.playerTakeExpEvent.Invoke(_amounts[_index]); // called by playerManager.AddExperiencePoints
    }
}
