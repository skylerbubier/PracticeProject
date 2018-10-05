using UnityEngine;
using System.Collections;

public class InfiniteScroll : MonoBehaviour
{
    private const float SCROLL_DAMP = 0.1f;
    private Renderer _renderer;

    public float scrollSpeed = 1f;

    private void Start()
    {
        // Grab Renderer of Scrollin' Object
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // Move the offset of the tiling, then repeat on 1
        float offsetX = Mathf.Repeat(Time.time * scrollSpeed * SCROLL_DAMP, 1f);
        _renderer.material.SetTextureOffset("_MainTex", new Vector2(offsetX, 0));
    }
}