using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperController : MonoBehaviour
{
    private CapsuleCollider2D colliderKeeper;
    private Animator anim;
    private bool goRight;

    //PUBLIC
    public int life;
    public float speed;
    public int points = 5;
    public Transform a;
    public Transform b;
    public GameObject range;

    public AudioSource sound;

    void Start()
    {
        colliderKeeper = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sound =  GetComponent<AudioSource>();
        switch (ChooseDifficulty.GetSavedDifficulty())
        {
            case ChooseDifficulty.DifficultyLevel.Easy:
                life = 2; 
                break;

            case ChooseDifficulty.DifficultyLevel.Normal:
                life = 4; 
                break;

            case ChooseDifficulty.DifficultyLevel.Hard:
                life = 6; 
                break;
        }

    }

    void Update()
    {
        if (life <= 0)
        {
            this.enabled = false;
            colliderKeeper.enabled = false;
            range.SetActive(false);
            anim.Play("dead", -1);

            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(points);
            }

            Destroy(gameObject, 1f);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            sound.Play();
            return;
        }

        if (goRight == true)
        {
            if (Vector2.Distance(transform.position, b.position) < 0.1f)
            {
                goRight = false;
            }

            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            transform.position = Vector2.MoveTowards(transform.position, b.position, speed * Time.deltaTime);
        }
        else
        {
            if (Vector2.Distance(transform.position, a.position) < 0.1f)
            {
                goRight = true;
            }

            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            transform.position = Vector2.MoveTowards(transform.position, a.position, speed * Time.deltaTime);
        }
    }
}
