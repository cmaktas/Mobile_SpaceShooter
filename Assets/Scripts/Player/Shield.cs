using UnityEngine;

public class Shield : MonoBehaviour
{
    private int hitsToDestroy = 3;
    public bool protection = false;

    [SerializeField] private GameObject[] shieldIcons;

    void OnEnable()
    {
        hitsToDestroy = 3;
        protection = true;
        UpdateUI();
    }

    private void UpdateUI()
    {
        switch (hitsToDestroy)
        {
            case 0:
                foreach (GameObject icon in shieldIcons)
                {
                    icon.SetActive(false);
                }
                break;
            case 1:
                shieldIcons[0].SetActive(true);
                shieldIcons[1].SetActive(false);
                shieldIcons[2].SetActive(false);
                break;
            case 2:
                shieldIcons[0].SetActive(true);
                shieldIcons[1].SetActive(true);
                shieldIcons[2].SetActive(false);
                break;
            case 3:
                shieldIcons[0].SetActive(true);
                shieldIcons[1].SetActive(true);
                shieldIcons[2].SetActive(true);
                break;
            default:
                Debug.Log("Shield ıcons switch statement error!");
                break;
        }
    }

    private void DamageShield()
    {
        hitsToDestroy -= 1;
        if (hitsToDestroy <= 0)
        {
            hitsToDestroy = 0;
            protection = false;
            gameObject.SetActive(false);
        }
        UpdateUI();
    }

    public void RepairShield()
    {
        hitsToDestroy = 3;
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Enemy enemy))
        {
            if (collider.CompareTag("Boss"))
            {
                hitsToDestroy = 0;
                DamageShield();
                return;
            }
            enemy.TakeDamage(10000);
            DamageShield();
        }
        else
        {
            Destroy(collider.gameObject);
            DamageShield();
        }
    }
}
