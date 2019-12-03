using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;

    private void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        //TODO: Make it so that the camera doesnt follow the player 1 to 1
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
