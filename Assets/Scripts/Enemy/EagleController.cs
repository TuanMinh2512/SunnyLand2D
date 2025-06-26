using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : Enemy
{
    [SerializeField] private Vector2 offsetUpRight;
    [SerializeField] private Vector2 offsetDownLeft;
    [SerializeField] private float flySpeed = 2f;
    [SerializeField] private float pauseDuration = 1f;

    private Vector3 pointA;
    private Vector3 pointB;
    private bool movingToB = true;

    protected override void Start()
    {
        base.Start();

        pointA = transform.position + (Vector3)offsetUpRight;
        pointB = transform.position + (Vector3)offsetDownLeft;

        StartCoroutine(FlyLoop());
    }

    private IEnumerator FlyLoop()
    {
        while (true)
        {
            Vector3 target = movingToB ? pointB : pointA;

            if (movingToB)
            {
                animator.ResetTrigger("FlyBack");
                animator.SetTrigger("Dive");
            }
            else
            {
                animator.ResetTrigger("Dive");
                animator.SetTrigger("FlyBack");
            }

            while (Vector3.Distance(transform.position, target) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, flySpeed * Time.deltaTime);

                transform.localScale = new Vector2(
                    target.x < transform.position.x ? 1 : -1,
                    1
                );

                yield return null;
            }

            yield return new WaitForSeconds(pauseDuration);
            movingToB = !movingToB;
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
        StartCoroutine(FlyLoop());
    }
}
