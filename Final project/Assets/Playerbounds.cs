using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerbounds : MonoBehaviour
{
    public float minX = -25f; 
    public float maxX = 25f; 
    public float minY = -25f;
    public float maxY = 25f;

    void LateUpdate()
    {
        Vector2 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);

        currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);

        transform.position = currentPosition;
    }
}