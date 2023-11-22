using UnityEngine;

public class Player : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        // Player damage logic
    }
}
