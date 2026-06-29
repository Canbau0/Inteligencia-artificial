using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float lifeTime = 3f;
    [SerializeField] int damage = 1;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity= transform.forward * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HealthController health = collision.gameObject.GetComponent<HealthController>();

        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }        
    }
}