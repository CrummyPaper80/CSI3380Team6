using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashIndicator : MonoBehaviour
{
    [Header("Settings")] 
    [Tooltip("The color of the indicator when a dash is available")]
    [SerializeField] private Color trueColor;
    [Tooltip("The color of the indicator when a dash is not available")]
    [SerializeField] private Color falseColor;

    [Header("References")] 
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SpriteRenderer indicatorSprite;
    
    private void Update()
    {
        if (playerController.dashes > 0)
        {
            indicatorSprite.color = trueColor;
        }
        else
        {
            indicatorSprite.color = falseColor;
        }
    }
}
