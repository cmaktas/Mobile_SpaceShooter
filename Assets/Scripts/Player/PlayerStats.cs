using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthFill;
    [SerializeField] GameObject explosionVFXPrefab;
    [SerializeField] Animator animator;
    [SerializeField] private Shield shield;

    private PlayerShooting playerShooting;

    private bool canPlayAnim = true;
    public bool canTakeDamage = true;
    private float health;

    private void Start()
    {
        playerShooting = GetComponent<PlayerShooting>();
        EndGameManager.endGameManager.RegisterPlayerStats(this);
        EndGameManager.endGameManager.possibleWin = false;
    }

    private void OnEnable()
    {
        health = maxHealth;
        UpdateHealthBar();
        EndGameManager.endGameManager.gameOver = false;
        StartCoroutine(DamageProtection());
    }

    IEnumerator DamageProtection()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(2f);
        canTakeDamage = true;
    }

    public void PlayerTakeDamage(float damage)
    {

        if (shield.protection || !canTakeDamage) return;
        health -= damage;
        UpdateHealthBar();

        if (canPlayAnim)
        {
            animator.SetTrigger("damage");
            StartCoroutine(AntiSpamAnimation());
        }
        playerShooting.DecreaseCanonLevel();
        if (health <= 0)
        {
            EndGameManager.endGameManager.gameOver = true;
            EndGameManager.endGameManager.StartResolveSequence();
            Instantiate(explosionVFXPrefab, transform.position, transform.rotation);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void UpdateHealthBar()
    {
        healthFill.fillAmount = health / maxHealth;
    }

    public void AddHealth(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        UpdateHealthBar();
    }

    private IEnumerator AntiSpamAnimation()
    {
        canPlayAnim = false;
        yield return new WaitForSeconds(0.15f);
        canPlayAnim = true;
    }
}
