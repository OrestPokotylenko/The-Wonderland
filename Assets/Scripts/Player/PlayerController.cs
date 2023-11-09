using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;

    private Vector2 input;

    private float sensitivity = 0.1f;

    private Animator animator;

    public LayerMask solidObjectsLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal") * sensitivity;
            input.y = Input.GetAxisRaw("Vertical") * sensitivity;

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPosition = transform.position;
                targetPosition.x += input.x;
                targetPosition.y += input.y;

                if (IsWalkable(targetPosition))
                    StartCoroutine(Move(targetPosition));
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPosition)
    {
        isMoving = true;

        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPosition)
    {
        if (Physics2D.OverlapCircle(targetPosition,0.05f,solidObjectsLayer) != null)
        {
            return false;
        }

        return true;
    }
}
