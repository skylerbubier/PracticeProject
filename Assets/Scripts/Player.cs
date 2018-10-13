using System;
using System.Collections.Generic;

using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 1.0f;
    public float fireRate = 1.0f;
    public float baseDamage = 10.0f;

    public Weapon[] weapons;

    private List<Weapon> _weapons = new List<Weapon>();

    public List<Weapon> Weapons
    {
        get { return _weapons; }
    }

    private void Start()
    {
        foreach (var w in weapons)
        {
            var weapon = Instantiate(w);
            weapon.name = w.name;
            weapon.transform.SetParent(transform);
            _weapons.Add(weapon);
        }
    }

}
