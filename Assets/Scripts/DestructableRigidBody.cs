using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableRigidBody : MonoBehaviour
{
    [SerializeField]
    Vector2 forceDirection;

    [SerializeField]
    float torque;

    Rigidbody2D rb2d;


    void Start()
    {
        float randTorque = UnityEngine.Random.Range(-100, 100);

        float randForceX = UnityEngine.Random.Range(forceDirection.x - 50, forceDirection.x + 50);
        float randForceY = UnityEngine.Random.Range(forceDirection.y - 50, forceDirection.x + 50);

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(forceDirection);
        rb2d.AddTorque(randTorque);
    }
}
