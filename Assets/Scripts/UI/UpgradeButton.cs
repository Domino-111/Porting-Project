using UnityEngine;
using TMPro;

public class UpgradeButton : MonoBehaviour, IStop
{
    /// <summary>
    /// Defines the type of upgrade to be received
    /// </summary>
    public enum Type
    {
        HealthRestore,
        MissileSpeed,
        MissileCooldown,
    }

    [Tooltip("The TMPro object displaying text on the button")]
    [SerializeField] TextMeshProUGUI buttonLabel;

    [Tooltip("How fast the button should move along the conveyor")]
    [SerializeField] private float speed = 100f;

    //References to the planet and gun objects
    private Planet.Planet planet;
    private Planet.Gun planetGun;

    //The current type of this upgrade button
    private Type type;

    //How strong the upgrade is overall
    private int level;

    //How much scrap for this upgrade
    private int cost;

    //The actual adjustment to make to the upgraded value
    private float amount;

    // Start is called before the first frame update
    void Start()
    {
        planet = FindObjectOfType<Planet.Planet>();
        planetGun = planet.GetComponent<Planet.Gun>();
        buttonLabel = GetComponentInChildren<TextMeshProUGUI>();
        Initialise();
        Destroy(gameObject, 15f);
    }

    private void Initialise()
    {
        //Get a random type of upgrade,
        //excluding health if no damage has been taken
        type = (Type)Random.Range(planet.HealthCurrent < 3 ? 0 : 1, 3);

        //Set a random level
        level = Random.Range(1, 6);

        //Depending on the type, set our scrap cost,
        //the actual amount to upgrade by, and what we're displaying
        switch (type)
        {
            case Type.MissileSpeed:
                amount = 0.2f * level;
                cost = Random.Range(2, 7) * level;
                buttonLabel.text = $"+{level} Missile Speed";
                break;
            case Type.MissileCooldown:
                amount = 0.1f * level;
                cost = Random.Range(4, 10) * level;
                buttonLabel.text = $"+{level} Missile Cooldown";
                break;
            case Type.HealthRestore:
                cost = 50 + 10 * level;
                buttonLabel.text = $"+1 Life";
                break;
        }
        buttonLabel.text += $"\n\n{cost} Scrap";
    }

    // Update is called once per frame
    void Update()
    {
        //Move the button upwards
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    public void TryToPurchase()
    {
        //If we don't have enough scrap, don't proceed
        if (!Scrap.TryToSpend(cost))
            return;

        //Depending on our upgrade type, apply the correct upgrade
        switch (type)
        {
            case Type.MissileSpeed:
                planetGun.missileSpeed += amount;
                break;
            case Type.MissileCooldown:
                planetGun.cooldownMax = Mathf.Clamp(planetGun.cooldownMax-amount, 0.1f, float.PositiveInfinity);
                break;
            case Type.HealthRestore:
                planet.Heal();
                break;
        }

        //Clear the button from the conveyor
        Destroy(gameObject);
    }

    public void Stop()
    {
        //Clear the button from the conveyor
        Destroy(gameObject);
    }
}
