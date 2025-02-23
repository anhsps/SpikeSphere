using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    //[SerializeField] private float speed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        Vector3 targetPos = new Vector3(player.position.x + 5f, transform.position.y, -10f);
        transform.position = targetPos;
        //transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }
}
