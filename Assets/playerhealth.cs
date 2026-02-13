using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerhealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Slider healthBar;
    public TMP_Text healthText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    void UpdateUI()
    {
        healthBar.value = currentHealth;
        healthText.text = currentHealth + " / " + maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
    }
}