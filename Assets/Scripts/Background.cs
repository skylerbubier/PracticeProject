using System;
using System.Collections.Generic;

using UnityEngine;

public class Background : MonoBehaviour
{

    private List<BackgroundLayer> _layers = new List<BackgroundLayer>();

    public Player player;

    /// <summary>
    /// List of all layers within this background.
    /// </summary>
    public List<BackgroundLayer> Layers
    {
        get { return _layers; }
    }

    private void Start()
    {
        // Find and initialize player automatically
        var potentialPlayer = GameObject.FindGameObjectWithTag("Player");
        if (potentialPlayer != null)
            player = potentialPlayer.GetComponent<Player>();

        // Find all background layers
        foreach (var layer in GetComponentsInChildren<BackgroundLayer>())
            AddLayer(layer);
    }

    /// <summary>
    /// Adds a Background Layer to this background.
    /// </summary>
    /// <param name="layer"></param>
    public void AddLayer(BackgroundLayer layer)
    {
        if (_layers.Contains(layer))
            return;
        _layers.Add(layer);
        layer.Background = this;
    }

    private void Update()
    {
        foreach (var layer in _layers)
        {
            var layerOffset = Mathf.Repeat(Time.time * layer.scrollSpeed, 1);
            if (player != null)
            {
                layerOffset = Mathf.Repeat(layerOffset * player.speed, 1);
            }
            layer.Renderer.material.SetTextureOffset("_MainTex", new Vector2(layerOffset, 0));
        }
    }

}
