using System;
using System.Collections.Generic;

using UnityEngine;

public class GameLevel : MonoBehaviour
{

    public Player playerPrefab;

    private Player _player;

    public Player Player
    {
        get { return _player; }
    }

    private void Start()
    {
        _player = Instantiate(playerPrefab);
        _player.name = "Player";
    }

    private void Update()
    {
        TEST_AsteroidSpawner();
    }

    public TargetSprite testEnemy;

    private float asteroidFire = 5.0f;
    private void TEST_AsteroidSpawner()
    {
        asteroidFire -= Time.deltaTime;
        if (asteroidFire <= 0)
        {
            asteroidFire += 4.0f;
            CreateEnemy(testEnemy);
        }
    }

    public void CreateEnemy(TargetSprite enemy)
    {
        var newEnemy = Instantiate(enemy);
        newEnemy.transform.SetParent(GameObject.FindGameObjectWithTag("Enemies").transform);
    }

}