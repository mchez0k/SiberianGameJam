using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private bool isFireBall;

    public void Initialize(Vector3 direction)
    {
        rb.AddForce(direction * speed, ForceMode.Impulse);
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isFireBall)
        {
            if (collision.collider.tag == "Hero") collision.collider.GetComponent<HeroMovement>().Restart();
            Destroy(gameObject);
        }
    }
}
