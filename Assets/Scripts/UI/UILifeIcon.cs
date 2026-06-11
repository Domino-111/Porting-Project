using UnityEngine;
using UnityEngine.UI;

public class UILifeIcon : MonoBehaviour
{
    [Tooltip("How many lives are required to display this icon")]
    [SerializeField] private int lifeThreshold;

    //Get a reference to the planet
    private Planet.Planet planet;

    //The image to display or not based on health
    private Image icon; 

    // Start is called before the first frame update
    void Start()
    {
        //Initialise our references
        planet = FindObjectOfType<Planet.Planet>();
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set the status of the icon based on our current health
        icon.enabled = planet.HealthCurrent > lifeThreshold;
    }
}
