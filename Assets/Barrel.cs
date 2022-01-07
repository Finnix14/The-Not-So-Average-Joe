using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{ 
    
    
    
    int health = 1;
    
    [SerializeField]
    UnityEngine.Object destructableRef;

    [SerializeField]
    UnityEngine.Object explosionRef;

    bool isShaking = false;
    float shakeAmount = .8f;

    Vector2 startPos;

    void Update()
    {
        if (isShaking)
        {
            transform.position = startPos + UnityEngine.Random.insideUnitCircle * shakeAmount;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            health--;

            if(health <= 0)
            {
                Explode();
            }
            else
            {
                isShaking = true;
                Invoke("ResetShake", .2f);
            }

        }
    }
    void ResetShake()
    {
        isShaking = false;
        transform.position = startPos;
    }
    void Explode()
    {
        
        GameObject destructable = (GameObject)Instantiate(destructableRef);
        GameObject particles = (GameObject)Instantiate(explosionRef);

        //map the new loaded destructable to the x/y position
        destructable.transform.position = transform.position;
        particles.transform.position = transform.position;

        Destroy(gameObject);
    }
    
}