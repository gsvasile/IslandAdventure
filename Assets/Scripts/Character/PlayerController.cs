using UnityEngine;

namespace RPG.Character
{
    public class PlayerController : MonoBehaviour
    {
        private Health healthComponent;
        private Combat combatComponent;

        [field: SerializeField] public CharacterStatsSO Stats { get; set; }

        private void Awake()
        {
            if (Stats == null)
            {
                Debug.LogWarning($"{name} does not have stats.");
            }

            healthComponent = GetComponent<Health>();
            combatComponent = GetComponent<Combat>();
        }

        private void Start()
        {
            healthComponent.HealthPoints = Stats.Health;
            combatComponent.Damage = Stats.Damage;
        }
    }
}
