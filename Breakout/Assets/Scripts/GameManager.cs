using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Unity objects")]
    [SerializeField] private Rigidbody2D ball;
    [SerializeField] private Transform paddle;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private GameObject winText;

    [Header("game parameters")]
    [SerializeField] private float paddleSpeed = 4f;
    [SerializeField] private Vector2 initialBallVelocity = new(2, 4);

    private bool isBallInPlay = false;
    private int score = 0;

    private Block[] blocks;
    private int totalBlocks;
    private int blocksRemaining;

    private void Start()
    {
        blocks = FindObjectsOfType<Block>();
        foreach(Block block in blocks)
        {
            if (block.isDestructable)
            {
                totalBlocks++;
            }
        }
        blocksRemaining = totalBlocks;
    }


    public void BlockDestroyed(int blockScore)
    {
        score += blockScore;
        scoreText.text = score.ToString();

        blocksRemaining--;

        if(blocksRemaining == 0)
        {
            ResetBall();
            winText.SetActive(true);
        }
    }

    public void ResetBall()
    {
        ball.velocity = Vector2.zero;
        isBallInPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        float moveAmount = direction * paddleSpeed * Time.deltaTime;
        paddle.Translate(moveAmount * Vector2.right);

        if (paddle.position.x < -5)
        {
            paddle.position = new Vector2(-5, paddle.position.y);
        } else if (paddle.position.x > 5)
        {
            paddle.position = new Vector2(5, paddle.position.y);
        }

        if (!isBallInPlay)
        {
            ball.transform.position = paddle.position + (Vector3.up * 0.2f);

            if (Input.GetButtonDown("Fire1"))
            {
                isBallInPlay = true;
                ball.AddForce(initialBallVelocity, ForceMode2D.Impulse);
            }
        }
    }
}
