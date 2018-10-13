using System;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TargetSprite : MonoBehaviour
{

    public float health = 10f;

    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If anything is colliding
    }

    public void Hit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Destroyed " + name);
            Destroy(gameObject);
        }
    }

}
