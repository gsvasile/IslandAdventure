using RPG.Utility;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        public Vector3 OriginalForwardVector { get; set; }
        public bool IsMoving { get; set; } = false;

        private NavMeshAgent agent;
        private Animator animatorComponent;

        private Vector3 movementVector;
        private bool clampAnimatorSpeedAgain = true;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animatorComponent = GetComponentInChildren<Animator>();

            OriginalForwardVector = transform.forward;
        }

        private void Start()
        {
            agent.updateRotation = false;
        }

        private void Update()
        {
            MovePlayer();
            MovementAnimator();
            if (CompareTag(Constants.PLAYER_TAG))
            {
                Rotate(movementVector);
            }
        }

        private void MovePlayer()
        {
            Vector3 offset = movementVector * Time.deltaTime * agent.speed;
            agent.Move(offset);
        }

        public void Rotate(Vector3 newForwardVector)
        {
            if (newForwardVector == Vector3.zero) return;

            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(newForwardVector);

            transform.rotation = Quaternion.Lerp(startRotation,
                                                 endRotation,
                                                 Time.deltaTime * agent.angularSpeed);
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                IsMoving = true;
            }

            if (context.canceled)
            {
                IsMoving = false;
            }

            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new Vector3(input.x, 0, input.y);
        }

        public void MoveAgentByDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
            IsMoving = true;
        }

        public void StopMovingAgent()
        {
            agent.ResetPath();
            IsMoving = false;
        }

        public bool ReachedDestination()
        {
            return !agent.pathPending &&
                    agent.remainingDistance <= agent.stoppingDistance &&
                    !agent.hasPath &&
                    agent.velocity.sqrMagnitude == 0f;
        }

        public void MoveAgentByOffset(Vector3 offset)
        {
            agent.Move(offset);
            IsMoving = true;
        }

        public void UpdateAgentSpeed(float newSpeed, bool shouldClampSpeed)
        {
            agent.speed = newSpeed;
            clampAnimatorSpeedAgain = shouldClampSpeed;
        }

        private void MovementAnimator()
        {
            float speed = animatorComponent.GetFloat(Constants.ANIMATOR_PARAMETER_SPEED);
            float smoothening = Time.deltaTime * agent.acceleration;

            if (IsMoving)
            {
                speed += smoothening;
            }
            else
            {
                speed -= smoothening;
            }

            speed = Mathf.Clamp01(speed);

            if (CompareTag(Constants.ENEMY_TAG) && clampAnimatorSpeedAgain)
            {
                speed = Mathf.Clamp(speed, 0f, 0.5f);
            }

            animatorComponent.SetFloat(Constants.ANIMATOR_PARAMETER_SPEED, speed);
        }
    }
}