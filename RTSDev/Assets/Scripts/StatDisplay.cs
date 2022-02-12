using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    public float maxHealth, currentHealth;

    [SerializeField] private Image healthBarAmount;

    // Update is called once per frame
    private void Update()
    {
        HandleHeath();
    }

    public void takeDamage(float damage)
    {
        float totalDamage = damage;
        currentHealth -= totalDamage;
    }

    private void HandleHeath()
    {
        Camera camera = Camera.main;
        gameObject.transform.LookAt(gameObject.transform.position +
            camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);

        healthBarAmount.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }
}
