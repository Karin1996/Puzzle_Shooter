using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 10f;

    public void TakeDamage(float amount) //get the damage from the gun
    {
        health -= amount;
        if(health <= 0f)
        {
            Die();
        }
    } 

    void Die()
    {
        Destroy(gameObject);
    }
}
