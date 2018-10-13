using System;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Shot : MonoBehaviour
{

    private const float SHOT_LIFE = 3.0f;
    private const float SHOT_MOVEMENT = 5.0f;

    public float shotSpeed = 1.0f;
    public float shotDamageMultiplier = 1.0f;
    public bool isPenetrating = false;

    private float shotLife = 0f;

    private float shotDamage = 10f;

    public void SetDamage(float damage)
    {
        shotDamage = damage * shotDamageMultiplier;
    }

    private void Update()
    {
        shotLife += Time.deltaTime;
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + (shotSpeed * SHOT_MOVEMENT * Time.deltaTime),
            transform.position.z);
        if (shotLife > SHOT_LIFE)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var target = collision.rigidbody.GetComponent<TargetSprite>();
        if (target != null)
        {
            target.Hit(shotDamage);
        }
        if (!isPenetrating)
            Destroy(gameObject);
    }

}
