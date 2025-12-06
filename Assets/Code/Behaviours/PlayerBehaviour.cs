using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private enum State
    {
        Left,
        Right
    }

    [SerializeField] private float _speed = 5f;
    [SerializeField] private GameObject _kiwiProjectile;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector2 _moveInput;
    private Vector2 _velocity;
    private SpriteRenderer _spriteRenderer;

    private static State _state;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 move = new Vector2(_moveInput.x, 0);
        _rigidbody.linearVelocity = move * _speed;
        UpdateAnimation(move);
        HandleKiwiShooting();
    }

    private void HandleKiwiShooting()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(_kiwiProjectile, this.gameObject.transform.position, Quaternion.identity);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    private void UpdateAnimation(Vector2 move)
    {
        _animator.SetFloat("MoveX", move.x);
        _animator.SetBool("IsMoving", Mathf.Abs(move.x) > 0.01f);

        if (move.x < 0)
        {
            _spriteRenderer.flipX = true;
            _animator.SetInteger("MoveXINT", 1);
        }
        else if (move.x == 0)
        {
            _animator.SetInteger("MoveXINT", 0);
        }
        else
        {
            _spriteRenderer.flipX = false;
            _animator.SetInteger("MoveXINT", 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<KiwiBehaviour>(out var kiwiProj)){
            kiwiProj.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }
}
