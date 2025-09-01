using UnityEngine;

public class Meteor : Enemy
{

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private PowerUpSpawner powerUpSpawner;

    private float speed;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        rb2D.linearVelocity = Vector2.down * speed;
    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }


    public override void HurtSequence()
    {
        base.HurtSequence();
    }

    public override void DeathSequence()
    {
        if (powerUpSpawner is not null)
            powerUpSpawner.SpawnPowerUp(transform.position);     
        base.DeathSequence();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerStats playerStats = collider.GetComponent<PlayerStats>();
            playerStats.PlayerTakeDamage(damage);
            DeathSequence();
        }
    }   
}
