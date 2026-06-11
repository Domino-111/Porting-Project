using UnityEngine;

namespace Planet
{
    public class Planet : MonoBehaviour, IReset
    {
        [Tooltip("How many hits the player can take before game over")]
        [SerializeField] private int healthMax = 3;
        
        //How many hits the player has left this round
        private int healthCurrent;

        /// <summary>
        /// Get how many hits the player has left this round.
        /// </summary>
        public int HealthCurrent => healthCurrent;

        // Start is called before the first frame update
        void Start()
        {
            Initialise();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //If the other object is an enemy
            if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                //Destroy the enemy and take damage
                Destroy(enemy.gameObject);
                TakeDamage(1);
            }
        }

        public void Initialise()
        {
            //Reset our health to maximum
            healthCurrent = healthMax;
        }

        private void TakeDamage(int amount)
        {
            //Reduce the amount of health we have
            healthCurrent = (int)Mathf.MoveTowards(healthCurrent, 0, amount);

            //If we're out of health, trigger game over
            if (healthCurrent == 0)
            {
                EndOfLife();
            }
        }

        public void Heal()
        {
            //Restore one health towards maximum.
            healthCurrent = Mathf.Clamp(healthCurrent+1, 0, healthMax);
        }

        private void EndOfLife()
        {
            //Trigger game over
            Game.Over();
        }

        public void Reset()
        {
            Initialise();
        }
    }
}