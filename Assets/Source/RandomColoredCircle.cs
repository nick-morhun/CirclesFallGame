﻿using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public class RandomColoredCircle : MonoBehaviour
{
    private bool isInitialized = false;

    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Initializes this circle's scale and color.
    /// </summary>
    public void Initialize(float scale)
    {
        if (isInitialized)
        {
            return;
        }

        transform.localScale = new Vector3(scale, scale, 1);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(Random.value, Random.value, Random.value);
        isInitialized = true;
    }

}
