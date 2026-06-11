using UnityEngine;
using TMPro;

public class UIScrapCount : MonoBehaviour
{
    [Tooltip("The TMPro object which will display the scrap amount")]
    [SerializeField] private TextMeshProUGUI label;

    // Start is called before the first frame update
    void Start()
    {
        label = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //Display the amount of scrap the player currently has
        label.text = "Scrap:\n" + Scrap.Amount.ToString();
    }
}
