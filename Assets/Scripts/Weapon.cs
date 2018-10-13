using System;
using System.Collections.Generic;

using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Shot shot;
    public float fireRate = 1.0f;

    public void Fire(Player player)
    {
        var projectiles = GameObject.FindGameObjectWithTag("Projectiles");
        var parentTransform = projectiles == null ? null : projectiles.transform;
        var newShot = Instantiate(shot);
        newShot.SetDamage(player.baseDamage);
        newShot.transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y + 1f,
            player.transform.position.z);
        newShot.transform.SetParent(parentTransform);
    }

}
