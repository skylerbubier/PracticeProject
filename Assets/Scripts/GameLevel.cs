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
    }

}