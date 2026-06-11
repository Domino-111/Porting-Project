using UnityEngine;
using UnityEngine.UI;

public class UICooldown : MonoBehaviour
{
    //The player's gun
    private Planet.Gun gun;
    //The cooldown bar to fill
    private Image image;

    [Tooltip("The gradient for the cooldown bar when charging")]
    [SerializeField] private Gradient chargingGradient;
    [Tooltip("The color for the cooldown bar when fully charged")]
    [SerializeField] private Color fullChargeColor;

    void Start()
    {
        //Get the image to fill
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure we have a reference to the gun
        if (gun == null)
            gun = FindObjectOfType<Planet.Gun>();

        //Fill the image based on the gun cooldown
        image.fillAmount = gun.CooldownPercent;

        //Either evaluate the gradient or display the full charge color as required
        if (gun.CooldownPercent < 1f)
            image.color = chargingGradient.Evaluate(gun.CooldownPercent);
        else
            image.color = fullChargeColor;
    }
}
