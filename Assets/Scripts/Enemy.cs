using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBeforeShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 8f;
    [SerializeField] float projectilePadding = 0.7f;

    private void Start()
    {
        InitShotCounter();
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            HandleHit(damageDealer);
        }
    }

    private void InitShotCounter()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBeforeShots, maxTimeBetweenShots);
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            InitShotCounter();
        }
    }

    private void Fire()
    {
        var laserPosition = new Vector2(transform.position.x, transform.position.y - projectilePadding);
        GameObject laser = Instantiate(laserPrefab, laserPosition, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void HandleHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
