using Assets.Code.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class KiwiBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _speed = 5f;
    private Vector2 moveDirection;
    private float force = 20f;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        SetStraigthVelocity();
    }

    void Update()
    {

    }

    void SetStraigthVelocity()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        moveDirection = (mouseWorldPos - (Vector2)transform.position).normalized;

        _rb.AddForce(moveDirection * _speed * force, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.GetComponent<CircleCollider2D>().isTrigger = true;

        if (collision.gameObject.TryGetComponent<PlayerBehaviour>(out var player))
        {
            
        }
        else
        {
            var firstContact = collision.contacts[0];
            _rb.AddForce(Vector2.Reflect(moveDirection, firstContact.normal) * _speed * (force / 2), ForceMode2D.Force);
            force /= 2;
        }
    }

    // TODO PRZEPISAC KOD COLLIZJI I TRIGGEROW BO TO JAKIES SPAGHETTI


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerBehaviour>(out var player))
        {
            this.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }

}
