using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private List<Transform> path;
    [SerializeField] private int currentTargetIndex = 0;
    [SerializeField] private Rigidbody rb;
    [SerializeField] public Transform startPoint;
    [SerializeField] public Transform endPoint;

    private Vector3 height = new Vector3(0f, 0.5f, 0f);

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GeneratePath();
        //FindObjectOfType<GhostAbilities>().OnNewBlockAdded.AddListener(GeneratePath);
    }

    private void GeneratePath()
    {
        path.Clear();
        Transform current = startPoint;

        while (current != endPoint)
        {
            path.Add(current);
            Block currentBlock = current.GetComponent<Block>();
            List<Transform> neighbors = new List<Transform>();

            // Получаем всех соседей текущего блока
            foreach (Collider collider in currentBlock.blocks)
            {
                Block neighbor = collider.GetComponent<Block>();
                if (neighbor != null && !path.Contains(neighbor.transform))
                {
                    neighbors.Add(neighbor.transform);
                }
            }

            if (neighbors.Count > 0)
            {
                // Выбираем случайного соседа
                current = neighbors[Random.Range(0, neighbors.Count)];
            }
            else
            {
                Debug.LogWarning("Тупик достигнут; путь не может продолжаться.");
                break;
            }
        }

        if (current == endPoint)
        {
            path.Add(endPoint);
        }
        else
        {
            Debug.LogWarning("Путь до конечной точки не найден. Генерируем новый маршрут");
            GeneratePath();
        }
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < 1f) rb.AddForce(Vector3.up * 100f, ForceMode.Force);
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
        Vector3 targetPosition = path[currentTargetIndex].position;
        Vector3 direction = (targetPosition - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        rb.MovePosition(newPosition);

        if (Vector3.Distance(transform.position, targetPosition) < 0.25f)
        {
            currentTargetIndex++;
        }
    }

    private void Restart()
    {
        GeneratePath();
        rb.velocity = Vector3.zero;
        transform.position = path[0].position + height;
        transform.rotation = Quaternion.identity;
        currentTargetIndex = 0;
    }
}
