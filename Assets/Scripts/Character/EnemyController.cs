using RPG.Utility;
using UnityEngine;

namespace RPG.Character
{
    public class EnemyController : MonoBehaviour
    {
        private Health healthComponent;
        public Combat CombatComponent { get; set; }

        [field: SerializeField] public CharacterStatsSO Stats { get; set; }
        [SerializeField] private float chaseRange = 2.5f;
        [SerializeField] private float attackRange = 0.75f;

        private AIBaseState currentState;

        public AIReturnState ReturnState => new AIReturnState();
        public AIChaseState ChaseState => new AIChaseState();
        public AIAttackState AttackState => new AIAttackState();
        public AIPatrolState PatrolState => new AIPatrolState();
        public AIDefeatedState DefeatedState => new AIDefeatedState();

        public float DistanceFromPlayer { get; private set; }
        public Vector3 OriginalPosition { get; private set; }
        public Movement MovementComponent { get; set; }
        public GameObject Player { get; private set; }
        public Patrol PatrolComponent { get; set; }

        public float ChaseRange => chaseRange;
        public float AttackRange => attackRange;

        private void Awake()
        {
            if (Stats == null)
            {
                Debug.LogWarning($"{name} does not have stats.");
            }

            currentState = ReturnState;

            Player = GameObject.FindWithTag(Constants.PLAYER_TAG);
            MovementComponent = GetComponent<Movement>();
            PatrolComponent = GetComponent<Patrol>();
            healthComponent = GetComponent<Health>();
            CombatComponent = GetComponent<Combat>();

            OriginalPosition = transform.position;
        }

        private void Start()
        {
            currentState.EnterState(this);

            healthComponent.HealthPoints = Stats.Health;
            CombatComponent.Damage = Stats.Damage;
        }

        private void OnEnable()
        {
            healthComponent.OnStartDefeated += HandleStartDefeated;
        }

        private void OnDisable()
        {
            healthComponent.OnStartDefeated -= HandleStartDefeated;
        }

        private void Update()
        {
            CalculateDistanceFromPlayer();

            currentState.UpdateState(this);
        }

        public void SwitchState(AIBaseState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

        private void CalculateDistanceFromPlayer()
        {
            if (Player == null)
            {
                return;
            }
            Vector3 enemyPosition = transform.position;
            Vector3 playerPosition = Player.transform.position;

            DistanceFromPlayer = Vector3.Distance(enemyPosition, playerPosition);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,
                                  chaseRange);
        }

        private void HandleStartDefeated()
        {
            SwitchState(DefeatedState);
            currentState.EnterState(this);
        }
    }
}