using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 500;
    [SerializeField] GameObject explosionPrefab;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 8f;
    [SerializeField] float projectileFiringPeriod = 0.3f;

    [Header("Sounds")]
    [SerializeField] AudioClip explosionClip;
    [Range(0.0f, 1f)] [SerializeField] float explosionVolume = 1f;
    [SerializeField] AudioClip shootingClip;
    [Range(0.0f, 1f)] [SerializeField] float shootingVolume = 1f;

    float xMin, xMax, yMin, yMax;
    Coroutine firingCoroutine;

    private void Start()
    {
        SetUpMoveBoundaries();
    }

    private void Update()
    {
        Move();

        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    public int GetHealth()
    {
        return health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            HandleHit(damageDealer);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            Fire();

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(shootingClip, Camera.main.transform.position, shootingVolume);
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
        FindObjectOfType<SceneLoader>().LoadGameOver();
        AudioSource.PlayClipAtPoint(explosionClip, Camera.main.transform.position, explosionVolume);
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 1f);
        Destroy(gameObject);
    }
}
