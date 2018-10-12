using System;
using System.Collections.Generic;

using UnityEngine;

public class BackgroundLayer : MonoBehaviour
{

    private Renderer _renderer;

    public float scrollSpeed = 1.0f;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public Renderer Renderer
    {
        get { return _renderer; }
    }

    private Background _background;

    public Background Background
    {
        get { return _background; }
        set { _background = value; }
    }

}