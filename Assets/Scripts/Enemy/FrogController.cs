using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : Enemy
{
    [SerializeField] private float leftPoint;
    [SerializeField] private float rightPoint;

    [SerializeField] private float jumpLength = 3f;
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float jumpInterval = 2f;
    [SerializeField] private LayerMask ground;

    private bool facingLeft = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(JumpLoop());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (isDead) return;

        //Transition from Jump to Fall
        if (animator.GetBool("Jumping") && rb.velocity.y < 0.1f)
        {
            animator.SetBool("Falling", true);
            animator.SetBool("Jumping", false);
        }

        //Transition from Fall to Idle
        if (animator.GetBool("Falling") && col.IsTouchingLayers(ground))
        {
            animator.SetBool("Falling", false);
        }
    }

    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftPoint)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector2(1, 1);
                }
                if (col.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    animator.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightPoint)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                if (col.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    animator.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player == null || !col.IsTouchingLayers(ground)) return;

        float dir = player.position.x - transform.position.x;
        float moveDir = Mathf.Sign(dir);

        rb.velocity = new Vector2(moveDir * jumpLength, jumpHeight);
        animator.SetBool("Jumping", true);

        transform.localScale = new Vector2(moveDir > 0 ? -1 : 1, 1);
    }

    private IEnumerator JumpLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(jumpInterval);

            if (isDead || !col.IsTouchingLayers(ground)) continue;

            if (isPlayerInRange)
                MoveTowardsPlayer();
            else
                Move();
        }
    }
    public override void JumpOn()
    {
        base.JumpOn();
        StopAllCoroutines();
    }

    public override void Revive()
    {
        base.Revive();
        StartCoroutine(JumpLoop());
    }
}
