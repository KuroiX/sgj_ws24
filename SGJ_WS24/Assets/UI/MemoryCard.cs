using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MemoryCard : MonoBehaviour
{
    public void Init(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}

