using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform targetEnemy;
    public float damage = 25;
    public float speed = 10f;
    [SerializeField] private string enemyTag;
    [SerializeField] private string laneEndTag;
    [SerializeField] private float lifeTimer = 5f;

    private void Start()
    {
        InvokeRepeating("CheckTarget", 2, 0.5f);
    }

    private void CheckTarget()
    {
        if (targetEnemy == null)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (targetEnemy != null)
        {
            SeekEnemy();
        }
        else
        {
            MoveStraight();
        }
    }

    private void MoveStraight()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void SeekEnemy()
    {
        Vector3 targetPosition = targetEnemy.position;
        targetPosition.y = transform.position.y;
        Vector3 direction = (targetEnemy.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(enemyTag))
        {
            collision.transform.GetComponent<Enemy>().Damage(damage);
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag(laneEndTag))
        {
            Destroy(gameObject);
        }
    }
}