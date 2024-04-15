using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollowplayer : MonoBehaviour
{
    public string targetTag = "Player";
    public float followSpeed = 2.2f;

    private Transform target;

    void LateUpdate()
    {
        GameObject player = GameObject.FindGameObjectWithTag(targetTag);
        if (player != null)
        {
            target = player.transform;
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        }
    }
}