using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
 
    void FixedUpdate()
    {
        this.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);
    }
}
