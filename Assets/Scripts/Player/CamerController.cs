using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    private float followSpeed = 2.5f;
    public Transform playerPosition;

    void Update()
    {
        Vector3 newPosition = new Vector3(playerPosition.position.x,playerPosition.position.y,-10f);
        transform.position = Vector3.Slerp(transform.position,newPosition,followSpeed*Time.deltaTime);
    }
}
