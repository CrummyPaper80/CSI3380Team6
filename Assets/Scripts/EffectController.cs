using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private float lifetime = 1f;

    // Will run as soon as the game object is instantiated
    private void Start()
    {
        // Destroys the game object after lifetime amount of seconds
        Destroy(gameObject, lifetime);
    }
}
