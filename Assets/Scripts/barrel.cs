using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel : MonoBehaviour
{
    bool isShaking = false;
    Vector2 startPos;
    [SerializeField]float shakeAmount = .2f;

    void Start()
    {
        startPos = transform.position;
    }


    void Update()
    {
        if (isShaking)
        {
            transform.position = startPos + UnityEngine.Random.insideUnitCircle * shakeAmount;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "AttackHitBox")
        {
            isShaking = true;
            Invoke("StopShaking", .2f);
        }
       
    }
    void StopShaking()
    {
        isShaking = false;
    }
}
