using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float defaultSpeed = 5.0f;
    public float defaultFocusSpeed = 3.0f;

    private Animator _animator;
    private Rigidbody2D _rigidbody2d;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _hitPointSpriteRenderer;

    private Vector2 _inputVector;
    private float _movementSpeed;
    private bool _focus;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _hitPointSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[1];
        _movementSpeed = defaultSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 movementVector = _inputVector.normalized * _movementSpeed * Time.fixedDeltaTime;
        _rigidbody2d.MovePosition(_rigidbody2d.position + movementVector);
    }

    private void LateUpdate()
    {
        _animator.SetFloat("Speed", _inputVector.magnitude);
        _animator.SetBool("Focus", _focus);

        if (_inputVector.x != 0)
        {
            _spriteRenderer.flipX = _inputVector.x > 0;
        }        
    }

    private void OnMove(InputValue inputValue)
    {
         _inputVector = inputValue.Get<Vector2>();
    }

    private void OnFocus(InputValue inputValue) 
    {
        if (inputValue.Get<float>() == 1)
        {
            FocusPressed();
        }
        else
        {
            FocusReleased();
        }
    }

    private void OnActiveWeapon(InputValue inputValue)
    {
        StartCoroutine(WeaponManager.instance.activeWeapon.Attack());
    }

    private void FocusPressed()
    {
        _focus = true;
        _movementSpeed = defaultFocusSpeed;
        _hitPointSpriteRenderer.enabled = true;
    }

    private void FocusReleased()
    {
        _focus = false;
        _movementSpeed = defaultSpeed;
        _hitPointSpriteRenderer.enabled = false;
    }
}
