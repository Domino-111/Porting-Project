using UnityEngine;
using UnityEngine.EventSystems;

namespace Planet
{
    public class Gun : MonoBehaviour, IReset
    {
        [Tooltip("The prefab of the missile for the player to shoot")]
        public Missile missilePrefab;
        [Tooltip("The new-game delay between missile shots")]
        [SerializeField] private float cooldownDefault = 2.5f;
        [Tooltip("The new-game speed of a missile")]
        [SerializeField] private float missileSpeedDefault = 1f;

        //These two variables hold the current missile speed and cooldown maximum, based on upgrades bought this round
        [HideInInspector] public float missileSpeed;
        [HideInInspector] public float cooldownMax;

        //How long is currently left before the player can shoot a missile
        private float cooldownCurrent;

        //The screen-position of world-position (0, 0, 0)
        private Vector3 screenCentre;

        // Dom Walsh - Creating screen size variables to track when screen size changes
        private float savedWidth, savedHeight;

        /// <summary>
        /// Returns a percent (0 - 1) of how long is left on the missile cooldown.
        /// </summary>
        public float CooldownPercent => 1 - (cooldownCurrent / cooldownMax);

        void Start()
        {
            Initialise();
            Reset();
        }

        void Update()
        {
            if (!Game.IsPlaying)
                return;

            //Count down the missile cooldown
            cooldownCurrent -= Time.deltaTime;

            //Try to shoot when the input is received.
            if (Input.GetButton("Shoot"))
                TryToShoot();

            // Dom Walsh - Check if the screen size has changed and re-initialise the centre of the screen to fix aiming
            if (Screen.width != savedWidth || Screen.height != savedHeight)
            {
                Initialise();
            }
        }

        void TryToShoot()
        {
            // Dom Walsh - Getting the fingerID if we're running on a mobile device
            int pointerID = -1;

            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.touchCount > 0)
                {
                    pointerID = Input.GetTouch(0).fingerId;
                }
            }

            //EventSystem.current.IsPointerOverGameObject() returns true if the mouse is over a UI element
            bool isOverUI = EventSystem.current.IsPointerOverGameObject(pointerID); // Dom Walsh - Pass the pointer ID in the GUI check value
            
            //If this is true, we don't want to act
            if (isOverUI)
                return;

            //If we're still on cooldown, don't act
            if (cooldownCurrent > 0)
                return;

            //Set our current cooldown to maximum
            cooldownCurrent = cooldownMax;

            //Get the direction for the missile, from screen centre to mouse position
            Vector3 direction = (Input.mousePosition - screenCentre).normalized;
            
            //Instantiate the missile, and set the direction and speed
            Missile missile = Instantiate(missilePrefab, transform.position + direction - Vector3.forward, Quaternion.identity);
            missile.transform.up = direction;
            missile.speed = missileSpeed;
        }

        public void Reset()
        {
            //Reset our current values to default
            missileSpeed = missileSpeedDefault;
            cooldownMax = cooldownDefault;
        }

        private void Initialise()
        {
            // Dom Walsh - Updating the saved values for the screen width and height
            savedWidth = Screen.width;
            savedHeight = Screen.height;

            //Figure out where the centre of the screen is
            screenCentre = new Vector2(Screen.width / 2f, Screen.height / 2f);
        }
    }
}