using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] int speed;
    Rigidbody2D rb;
    Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetVelocity(Vector2 _direc)
    {
        direction = _direc;
    }
    private void FixedUpdate()
    {
        rb.velocity = direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            Debug.Log("EnemigoEliminado");
            PlayerController.IncrementSP?.Invoke(1);
            Destroy(gameObject);
            Destroy(collision.gameObject);

        }
    }

}
