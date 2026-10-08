using UnityEngine;
using TMPro;

public class HealthPlayerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textHealth;
    [SerializeField] private TextMeshProUGUI textMaxHealth;

    public void ApplyCurrentHealth(int currentHealthPlayer)
    {
        if  (textHealth != null)
        {
            textHealth.text = currentHealthPlayer.ToString();
        }
    }
    
    public void ApplyMaxHealth(int maxHealthPlayer)
    {
        if (textMaxHealth != null)
        {
            textMaxHealth.text = maxHealthPlayer.ToString();
        }
    }
}
