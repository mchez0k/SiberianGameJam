using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public static HeroMovement Instance;
    [SerializeField] private float speed = 1.0f;
    private float defaultSpeed;
    private float stopSpeed;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private int stepsToPause = 4;

    [SerializeField] private List<Transform> path;
    [SerializeField] private GameObject[] models;
    private GameObject currentModel;
    [SerializeField] private int currentTargetIndex = 0;
    [SerializeField] private Rigidbody rb;
    [SerializeField] public Transform startPoint;
    [SerializeField] public Transform endPoint;

    [SerializeField] public TextMeshProUGUI nameText;
    [SerializeField] public TextMeshProUGUI descriptionText;

    private Vector3 height = new Vector3(0f, 0.5f, 0f);

    private void Start()
    {
        Instance = this;
        defaultSpeed = speed;
        stopSpeed = speed / 5f;
        currentModel = models[0];
        rb = GetComponent<Rigidbody>();
        nameText.text = Random.Range(0, 100).ToString();
        descriptionText.text = Random.Range(0, 100).ToString();
        GeneratePath();
    }

    private void GeneratePath(Transform startPoint = null)
    {
        path.Clear();
        Transform current;
        if (startPoint == null)
        {
            current = this.startPoint;
        } else
        {
            current = startPoint;
        }

        while (current != endPoint)
        {
            path.Add(current);
            Block currentBlock = current.GetComponent<Block>();
            List<Transform> neighbors = new List<Transform>();

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
                current = neighbors[Random.Range(0, neighbors.Count)];
            }
            else
            {
                break;
            }
        }

        if (current == endPoint)
        {
            path.Add(endPoint);
        }
        else
        {
            Debug.LogWarning("Generating a new path");
            GeneratePath(startPoint);
        }
    }

    private void FixedUpdate()

    {
        if (rb.velocity.magnitude < 1f)
        {
            rb.AddForce(Vector3.up * 100f, ForceMode.Force);
        }

        if (currentTargetIndex < path.Count)
        {
            MoveToTarget();
        }
        else
        {
            GameManager.LoadLevel();
        }

        //if (rb.velocity.magnitude < 1f)
        //{
        //    rb.AddForce(Vector3.up * 100f, ForceMode.Force);
        //}

        //if (currentTargetIndex < path.Count)
        //{
        //    if (currentTargetIndex % stepsToPause == 0 && currentTargetIndex > 0 && path[currentTargetIndex].GetComponent<Block>().BlockType != EBlockType.Abyss)
        //    {
        //        speed = stopSpeed;
        //    } else
        //    {
        //        speed = defaultSpeed;
        //    }
        //    MoveToTarget();
        //}
        //else
        //{
        //    GameManager.LoadLevel();
        //}
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

        if (CheckPosition(transform.position, targetPosition))
        {
            currentTargetIndex++;
        }
    }

    private bool CheckPosition(Vector3 heroPos, Vector3 targetPos)
    {
        return Mathf.Abs(heroPos.x - targetPos.x) < 0.3f && Mathf.Abs(heroPos.z - targetPos.z) < 0.3f;
    }

    public void Scream()
    {
        GeneratePath(path[currentTargetIndex]);
    }

    public void Restart()
    {
        GeneratePath();
        nameText.text = Random.Range(0, 100).ToString();
        descriptionText.text = Random.Range(0, 100).ToString();

        currentModel.SetActive(false);
        currentModel = models[Random.Range(0, models.Length)];
        currentModel.SetActive(true);
        rb.velocity = Vector3.zero;
        transform.position = path[0].position + height;
        transform.rotation = Quaternion.identity;
        currentTargetIndex = 0;
    }
}
