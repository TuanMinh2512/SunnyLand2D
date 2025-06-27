using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D col;
    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;

    private bool isGrounded;
    private Vector3 spawnPosition;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpforce = 10f;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource cherry;

    private const string CHERRY_TAG = "cherry";
    private const string ENEMY_TAG = "Enemy";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        spawnPosition = transform.position;
        PermanentUI.perm.SetDefaultSpawn(spawnPosition);
        PermanentUI.perm.LoadCheckpointState(gameObject);
        PermanentUI.perm.UpdateUI();
    }

    void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        animator.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(CHERRY_TAG))
        {
            cherry.Play();
            PermanentUI.perm.AddCherry();
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            spawnPosition = collision.transform.position;
            PermanentUI.perm.SaveCheckpointState(spawnPosition);
        }
        else if (collision.CompareTag("Finish"))
        {
            LevelTimer timer = FindObjectOfType<LevelTimer>();
            if (timer != null)
            {
                timer.CompleteLevel();
            }

            Scene currentScene = SceneManager.GetActiveScene();
            int nextSceneIndex = currentScene.buildIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                PermanentUI.perm.FullReset();
            }
        }
        else if (collision.CompareTag("LandDie"))
        {
            PermanentUI.perm.SubtractHealth();

            if (PermanentUI.perm.health <= 0)
            {
                SceneManager.LoadScene("GameOver"); // chuyen scene khi chet
            }
            else
            {
                PermanentUI.perm.LoadCheckpointState(gameObject);
                rb.velocity = Vector2.zero;
                state = State.idle;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(ENEMY_TAG))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (state == State.falling)
            {
                enemy?.JumpOn();
                Jump();
            }
            else
            {
                state = State.hurt;
                PermanentUI.perm.SubtractHealth();

                if (PermanentUI.perm.health <= 0)
                {
                    SceneManager.LoadScene("Retry");
                    return;
                }

                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f && ((1 << collision.gameObject.layer) & ground) != 0)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & ground) != 0)
        {
            isGrounded = false;
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0)
        {
            rb.velocity = new Vector2(hDirection * speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(hDirection * speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        state = State.jumping;
        isGrounded = false;
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (col.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void FootStep()
    {
        footstep.Play();
    }

    public void Respawn()
    {
        transform.position = spawnPosition;
        rb.velocity = Vector2.zero;
        state = State.idle;
    }
}
