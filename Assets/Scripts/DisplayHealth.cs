using TMPro;
using UnityEngine;

public class DisplayHealth : MonoBehaviour
{
    private Player player;
    private TextMeshProUGUI healthText;

    void Start()
    {
        player = FindObjectOfType<Player>();
        healthText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (player != null)
        {
            healthText.text = player.GetHealth().ToString();
        }
        else 
        {
            healthText.text = "0";
        }
    }
}
