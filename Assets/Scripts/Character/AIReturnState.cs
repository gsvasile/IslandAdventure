using UnityEngine;

namespace RPG.Character
{
    public class AIReturnState : AIBaseState
    {
        private Vector3 targetPosition;

        public override void EnterState(EnemyController enemy)
        {
            enemy.MovementComponent.UpdateAgentSpeed(enemy.Stats.WalkSpeed, true);

            if (enemy.PatrolComponent != null)
            {
                targetPosition = enemy.PatrolComponent.GetNextPosition();

                enemy.MovementComponent.MoveAgentByDestination(targetPosition);
            }
            else
            {
                enemy.MovementComponent.MoveAgentByDestination(enemy.OriginalPosition);
            }
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.DistanceFromPlayer < enemy.ChaseRange)
            {
                enemy.SwitchState(enemy.ChaseState);
                return;
            }

            if (enemy.MovementComponent.ReachedDestination())
            {
                if (enemy.PatrolComponent != null)
                {
                    enemy.SwitchState(enemy.PatrolState);
                    return;
                }
                else
                {
                    enemy.MovementComponent.IsMoving = false;
                    enemy.MovementComponent.Rotate(enemy.MovementComponent.OriginalForwardVector);
                }
            }
            else
            {
                if (enemy.PatrolComponent != null)
                {
                    Vector3 newForwardVector = targetPosition - enemy.transform.position;
                    newForwardVector.y = 0;

                    enemy.MovementComponent.Rotate(newForwardVector);
                }
                else
                {
                    Vector3 newForwardVector = enemy.OriginalPosition -
                                               enemy.transform.position;
                    newForwardVector.y = 0;

                    enemy.MovementComponent.Rotate(newForwardVector);
                }
            }

        }
    }
}