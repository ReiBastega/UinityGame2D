using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoController : MonoBehaviour
{   
    private CapsuleCollider2D colliderGizmo;
    private Animator anim;
    private float sideSing;
    private string side;

    public int life;
    public float speed;
    public int points = 10;

    public Transform player;

    public GameObject range;

    void Start()
    {
        colliderGizmo = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

        // Configurar vida baseada na dificuldade
        switch (ChooseDifficulty.GetSavedDifficulty())
        {
            case ChooseDifficulty.DifficultyLevel.Easy:
                life = 5; 
                break;

            case ChooseDifficulty.DifficultyLevel.Normal:
                life = 10;
                break;

            case ChooseDifficulty.DifficultyLevel.Hard:
                life = 15;
                break;
        }

    }

    void Update()
    {
        if (life <= 0)
        {
            this.enabled = false;
            colliderGizmo.enabled = false;
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
            return;
        }
        
        sideSing = Mathf.Sign(transform.position.x - player.position.x);

        if (Mathf.Abs(sideSing) == 1.0f)
        {
            side = sideSing == 1.0f ? "right" : "left";
        }
        
        switch(side)
        {
            case "right": 
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                break;
                
            case "left":
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                break;        
        }

        if (Vector2.Distance(transform.position, player.position) > 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.deltaTime);
        }
    }
}
