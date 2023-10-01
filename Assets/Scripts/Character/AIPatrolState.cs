using UnityEngine;

namespace RPG.Character
{
    public class AIPatrolState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.PatrolComponent.ResetTimers();
            enemy.PatrolComponent.ResetWalkAndPauseDurations();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.DistanceFromPlayer < enemy.ChaseRange)
            {
                enemy.SwitchState(enemy.ChaseState);
                return;
            }

            Vector3 oldPosition = enemy.PatrolComponent.GetNextPosition();

            enemy.PatrolComponent.CalculateNextPosition();

            Vector3 currentPosition = enemy.transform.position;
            Vector3 newPosition = enemy.PatrolComponent.GetNextPosition();
            Vector3 offset = newPosition - currentPosition;

            enemy.MovementComponent.MoveAgentByOffset(offset);

            Vector3 fartherOutPosition = enemy.PatrolComponent.GetFartherOutPosition();
            Vector3 newForwardVector = fartherOutPosition - currentPosition;
            // Ensure that the new vector is not looking up or down.
            newForwardVector.y = 0;
            enemy.MovementComponent.Rotate(newForwardVector);

            if (oldPosition == newPosition)
            {
                enemy.MovementComponent.IsMoving = false;
            }
        }
    }
}