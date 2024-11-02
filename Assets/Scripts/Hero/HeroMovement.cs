using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private List<Transform> path;
    [SerializeField] private int currentTargetIndex = 0;
    [SerializeField] private Rigidbody rb;

    private Vector3 height = new Vector3(0f, 1.3f, 0f);

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (currentTargetIndex < path.Count)
        {
            MoveToTarget();
        } else {
            Restart();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OutOfBounds"))
        {
            Restart();
        }
    }

    private void MoveToTarget()
    {
        Vector3 targetPosition = path[currentTargetIndex].position + height;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        rb.MovePosition(newPosition);

        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            currentTargetIndex++;
        }
    }

    private void Restart()
    {
        rb.velocity = Vector3.zero;
        transform.position = path[0].position + height;
        currentTargetIndex = 0;
    }
}
