using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float scrollSpeed;

    private new Renderer _renderer;
    private Vector2 _savedOffset;

    void Start () {
        _renderer = GetComponent<Renderer>();
    }

    void Update () {
        var x = Mathf.Repeat (Time.time * scrollSpeed, 1);
        var offset = new Vector2 (x, 0);
        _renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
