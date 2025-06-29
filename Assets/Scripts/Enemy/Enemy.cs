using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;
    protected AudioSource deathSound;
    protected Collider2D col;
    protected bool isDead = false;

    [SerializeField] private bool autoRespawn = false;
    [SerializeField] private float respawnDelay = 3f;

    [Header("Player Detection")]
    [SerializeField] protected float detectRange = 10f;
    protected Transform player;
    protected bool isPlayerInRange = false;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        deathSound = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    protected virtual void Update()
    {
        if (player != null && !isDead)
        {
            float distance = Vector2.Distance(player.position, transform.position);
            isPlayerInRange = distance <= detectRange;
        }
    }
    public virtual void JumpOn()
    {
        if (isDead) return;
        isDead = true;
        animator.SetTrigger("Death");
        deathSound.Play();
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        col.enabled = false;
    }

    public void Death()
    {
        if (autoRespawn)
            StartCoroutine(RespawnAfterDelay());
        else
            StartCoroutine(HideAfterAnimation());
    }

    private IEnumerator HideAfterAnimation()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;
        yield return null;
        gameObject.SetActive(false);
    }

    public virtual void Revive()
    {
        isDead = false;
        gameObject.SetActive(true);
        rb.bodyType = RigidbodyType2D.Dynamic;
        col.enabled = true;
        animator.Rebind();
        animator.Update(0f);

        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        Revive();
    }

}
