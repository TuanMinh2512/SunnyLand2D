using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : Enemy
{
    [SerializeField] private float dropSpeed = 5f;
    [SerializeField] private float triggerRange = 6f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform detectPoint;

    private bool isFalling = false;

    protected override void Start()
    {
        base.Start();
        rb.bodyType = RigidbodyType2D.Kinematic;
        animator.SetBool("IsHanging", true);
        animator.SetBool("IsFlying", false);
    }

    protected override void Update()
    {
        base.Update();

        if (!isFalling)
        {
            Collider2D player = Physics2D.OverlapCircle(detectPoint.position, triggerRange, playerLayer);
            if (player != null)
            {
                isFalling = true;
                animator.SetBool("IsHanging", false);
                animator.SetBool("IsFlying", true);

                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.velocity = new Vector2(0, -dropSpeed);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (detectPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectPoint.position, triggerRange);
    }

    public override void JumpOn()
    {
        base.JumpOn();
        rb.velocity = Vector2.zero;
    }

    public override void Revive()
    {
        base.Revive();
        isFalling = false;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;

        animator.SetBool("IsHanging", true);
        animator.SetBool("IsFlying", false);
    }
}
