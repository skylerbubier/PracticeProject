using System;
using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TargetSprite : MonoBehaviour
{

    public float health = 100f;

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

        //Play destroy sound, call destroy
        if (health <= 0)
        {
            var audioData = GetComponent<AudioSource>();
            audioData.Play(0);

            //disable graphics
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            //disable collision
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    IEnumerator Destroy(float time)
    {
        //Wait for sound to stop
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
