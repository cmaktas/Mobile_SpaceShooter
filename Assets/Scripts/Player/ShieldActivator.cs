using UnityEngine;

public class ShieldActivator : MonoBehaviour
{
    [SerializeField] private Shield shield;

    public void ActivateShield()
    {
        if (shield.gameObject.activeSelf == false)
        {
            shield.gameObject.SetActive(true);
        }
        else
        {
            shield.RepairShield();
        }
    }
}
