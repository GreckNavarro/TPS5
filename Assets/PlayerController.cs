using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int specialPower;
    [SerializeField] private float timeInv;
    [SerializeField] private float currentTime;
    [SerializeField] private GameObject prefabBullet;


    SpriteRenderer sp;
    CircleCollider2D cc2D;

    Vector2 AIM;


    

    public static event Action DesactiveSpecial;
    public static Action<int> IncrementSP;



    private void OnEnable()
    {
        GameManager.Special += SpecialActive;
        DesactiveSpecial += NormalState;
        IncrementSP += Increment;
    }

    private void OnDisable()
    {
        GameManager.Special -= SpecialActive;
        DesactiveSpecial -= NormalState;
        IncrementSP -= Increment;
    }
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        cc2D = GetComponent<CircleCollider2D>();

        currentTime = timeInv;
    }

    // Update is called once per frame
    void Update()
    {
          ValidateSpecialPower();
    }

    public void ValidateSpecialPower()
    {
        if(specialPower > 3)
        {
            GameManager.Special?.Invoke();
        }
    }

    private void SpecialActive()
    {
        sp.color = Color.green;
        cc2D.enabled = false;

        currentTime -= Time.deltaTime;
        if(currentTime <= 0)
        {
            DesactiveSpecial?.Invoke();
            specialPower = 0;
        }
    }

    private void NormalState()
    {
        cc2D.enabled = true;
        sp.color = Color.blue;
        currentTime = timeInv;

    }

    private void Increment(int a)
    {
        specialPower += a;
    }

    public void Aim(InputAction.CallbackContext value)
    {
         AIM = value.ReadValue<Vector2>();
    }

    public void Shoot(InputAction.CallbackContext value)
    {

        if (value.started)
        {
            Vector2 disparo = Camera.main.ScreenToWorldPoint(AIM);

            GameObject bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletController>().SetVelocity(disparo);

            Debug.Log(disparo);
        }

    }


}
