using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDamage;
    
    private void Start()
    {
        

    }
    
    public void ShowDamageText(int damage, Vector2 pos)
    {
        textDamage.gameObject.SetActive(true);
        textDamage.rectTransform.position = pos;
        textDamage.text = damage.ToString();
    }
    
}