using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private bool canMove;
    private int lastScore;

    // Start is called before the first frame update
    void Start()
    {
        lastScore = GameManager8.instance.score;
        Invoke("CanMove", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (TouchScreen.instance.click)
        {
            CancelInvoke("CanMove");
            canMove = true;
        }

        if (canMove)
            transform.position += Vector3.right * speed * Time.deltaTime;

        if (GameManager8.instance.score - lastScore >= 100)
        {
            speed += 1f;
            lastScore = GameManager8.instance.score;
        }
    }

    private void CanMove() => canMove = true;
}
