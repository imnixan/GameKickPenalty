using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] bodyParts;

    private Animator animator;

    private Vector3 startPos;

    private void Start()
    {
        bodyParts = GetComponentsInChildren<Rigidbody>();
        startPos = transform.position;
        animator = GetComponentInChildren<Animator>();
    }

    public void CatchBall(Vector3 ballPoint)
    {
        animator.enabled = false;
        Vector3 aimPoint = ballPoint;
        aimPoint.x += Random.Range(-0.5f, 0.5f);
        aimPoint.y += Random.Range(-0.5f, 0.5f);
        Vector3 force = aimPoint - transform.position;

        force.x *= 20;
        force.y *= 20;
        force.z *= 20;
        foreach (var bodyPart in bodyParts)
        {
            bodyPart.AddForce(force, ForceMode.Impulse);
        }
    }

    public void Respawn()
    {
        foreach (var bodyPart in bodyParts)
        {
            bodyPart.velocity = Vector3.zero;
            bodyPart.angularVelocity = Vector3.zero;
            bodyPart.MovePosition(startPos);
        }
        animator.enabled = true;
    }
}
