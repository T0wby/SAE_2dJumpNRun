using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [Range(1f, 10f)] public float smoothFactor;
    private Vector3 offset = new Vector3(0, 0, -10);
    private Vector3 playerOffset;
    private Vector3 smoothedPosition;

    void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        playerOffset = player.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, playerOffset, smoothFactor + Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
