using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Block parameters")]
    [SerializeField] private int hitsToDestroy = 1;
    [SerializeField] public bool isDestructable = true;
    [SerializeField] private int score = 10;

    private GameManager gameManager;

    private void Start()
    {
      gameManager = FindObjectOfType<GameManager>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDestructable)
        {
            hitsToDestroy--;

            if (hitsToDestroy == 0)
            {
                gameManager.BlockDestroyed(score);
                gameObject.SetActive(false);
            }
        }
    }
}
