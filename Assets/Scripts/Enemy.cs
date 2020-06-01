using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBeforeShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject explosionPrefab;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 8f;

    [Header("Sounds")]
    [SerializeField] AudioClip explosionSound;
    [Range(0.0f, 1f)] [SerializeField] float explosionVolume = 1f;
    [SerializeField] AudioClip shootingSound;
    [Range(0.0f, 1f)] [SerializeField] float shootingVolume = 1f;

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
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootingSound, Camera.main.transform.position, shootingVolume);
    }

    private void HandleHit(DamageDealer damageDealer)
    {
        damageDealer.Hit();
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionVolume);
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 1f);
        Destroy(gameObject);
    }
}
