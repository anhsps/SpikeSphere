using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player8 : MonoBehaviour
{
    [SerializeField] private Transform ground1, ground2, ground3;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Color[] colors;
    [SerializeField] private GameObject leftBtn, rightBtn;

    private float startX;
    private int score;
    private int index;

    SpriteRenderer sr;
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        sr = GetComponent<SpriteRenderer>();
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveObjs();
        PlayerScore();

        if (TouchScreen.instance.click)
        {
            leftBtn.SetActive(false);
            rightBtn.SetActive(false);
        }
    }

    private void MoveObjs()
    {
        if (transform.position.x > ground2.position.x)
            ground1.position = ground3.position + Vector3.right * 120;
        if (transform.position.x > ground3.position.x)
            ground2.position = ground1.position + Vector3.right * 120;
        if (transform.position.x > ground1.position.x && ground1.position.x > ground3.position.x)
            ground3.position = ground2.position + Vector3.right * 120;
    }

    private void PlayerScore()
    {
        int newScore = Mathf.FloorToInt(transform.position.x - startX) / 1;
        if (newScore > score)
        {
            score = newScore;
            GameManager8.instance.UpdateScore(newScore);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            GameManager8.instance.GameLose();
        }
    }

    public void RightButton()
    {
        index = (index + 1) % sprites.Length;
        UpdateSprite();
    }

    public void LeftButton()
    {
        index = (index + 3) % sprites.Length;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        sr.sprite = sprites[index];
        line.startColor = colors[index];
        line.endColor = colors[index];
    }
}
