using UnityEngine;

namespace RPG.Character
{
    public class AIChaseState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.MovementComponent.UpdateAgentSpeed(enemy.Stats.RunSpeed, false);
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.Player == null)
            {
                enemy.CombatComponent.CancelAttack();
                enemy.MovementComponent.StopMovingAgent();
                return;
            }

            if (enemy.DistanceFromPlayer > enemy.ChaseRange)
            {
                enemy.SwitchState(enemy.ReturnState);
                return;
            }

            if (enemy.DistanceFromPlayer < enemy.AttackRange)
            {
                enemy.SwitchState(enemy.AttackState);
                return;
            }

            enemy.MovementComponent.MoveAgentByDestination(enemy.Player.transform.position);

            Vector3 playerDirecton = enemy.Player.transform.position -
                                     enemy.transform.position;

            enemy.MovementComponent.Rotate(playerDirecton);
        }
    }

}