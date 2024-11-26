using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    // VARIAVEIS PRIVADAS
    private Rigidbody2D rb;
    private Animator anim;
    private float moveX;
    private CapsuleCollider2D colliderPlayer;

    // VARIAVEIS PUBLICAS
    [Header("Atributtes")]
    public float speed;
    public int addJump;
    public float jumpForce;
    public int life;
    private int initialLife;
    
    [Header("GroundDetection")]
    public LayerMask groundLayer;
    public float groundCheckDistance;
    public Vector2 groundCheckOffsetLeft;
    public Vector2 groundCheckOffsetRight;

    [Header("Bool")]
    [SerializeField]public bool isGrounded;
    [HideInInspector]public bool isPause;
    
    [Header("UI")]
    public TextMeshProUGUI textLife;
    
    [Header("GameObject")]
    public GameObject gameOver;
    public GameObject pauseGame;
    
    [Header("Sounds")]
    private AudioSource sound;
    
    [Header("Level")]
    public string levelName;

    void Start() {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        colliderPlayer = GetComponent<CapsuleCollider2D>();
        sound =  GetComponent<AudioSource>();
        initialLife = life;
    }
    void Update() {
        moveX = Input.GetAxisRaw("Horizontal");
        textLife.text = life.ToString();

        if (life <= 0) {
            this.enabled = false;
            rb.velocity = Vector2.zero;
            colliderPlayer.enabled = false;
            rb.gravityScale = 0;
            anim.Play("death", -1);
            gameOver.SetActive(true);
        }

        if(Input.GetButtonDown("Cancel")){
            PauseScreen();
        }


        if(isGrounded) {
            addJump = 1;
            if(Input.GetButtonDown("Jump")) {
                 Jump();
            }   
        } else {
           if(Input.GetButtonDown("Jump") && addJump > 0) {
                 addJump--;
                 Jump();
           }
        }
        Attack();
    }

    void FixedUpdate() {
        Move();
        CheckGroundedStatus();
    }

    void Move() {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if(moveX > 0){
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetBool("isRun", true);

            groundCheckOffsetLeft.x = -Mathf.Abs(groundCheckOffsetLeft.x);
            groundCheckOffsetRight.x = Mathf.Abs(groundCheckOffsetRight.x);
        } else if(moveX < 0){
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.SetBool("isRun", true);

            groundCheckOffsetLeft.x = Mathf.Abs(groundCheckOffsetLeft.x);
            groundCheckOffsetRight.x = -Mathf.Abs(groundCheckOffsetRight.x);
        } else{
            anim.SetBool("isRun", false);
        }
    }

    void Jump() {
        rb.velocity = new Vector2( rb.velocity.x, jumpForce);
         anim.SetBool("isJump", true);
    }

    public void ResetLife() {
        life = initialLife;
    }

    void PauseScreen(){
        if (isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            pauseGame.SetActive(false);
        } else{
            isPause = true;
            Time.timeScale = 0;
            pauseGame.SetActive(true);
        }
    }
    public void ResumeGame(){
            isPause = false;
            Time.timeScale = 1;
            pauseGame.SetActive(false);
    }

    public void BackToMenu(){
        SceneManager.LoadScene(0);
    }

   void Attack(){
        if(Input.GetButtonDown("Fire1")) {
            sound.Play();
            anim.Play("attack", -1);
        }
    }

    void CheckGroundedStatus() {
      Vector2 position = transform.position;
      Vector2 groundCheckPositiontLeft = position + groundCheckOffsetLeft;
      Vector2 groundCheckPositionRight = position + groundCheckOffsetRight;

      RaycastHit2D hitLeft = Physics2D.Raycast(groundCheckPositiontLeft, Vector2.down, groundCheckDistance,groundLayer);
      RaycastHit2D hitRight = Physics2D.Raycast(groundCheckPositionRight, Vector2.down, groundCheckDistance,groundLayer);

      isGrounded = hitLeft.collider != null || hitRight.collider != null;
      anim.SetBool("isJump", !isGrounded);
    }

    void OnDrawGizmos() {
        Vector3 groundCheckPositiontLeft = transform.position + new Vector3(groundCheckOffsetLeft.x, groundCheckOffsetLeft.y, 0f);
        Vector3 groundCheckPositionRight = transform.position + new Vector3(groundCheckOffsetRight.x, groundCheckOffsetRight.y, 0f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheckPositiontLeft, groundCheckPositiontLeft + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(groundCheckPositionRight, groundCheckPositionRight + Vector3.down * groundCheckDistance);
    }


}