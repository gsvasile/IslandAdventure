using UnityEngine;

namespace RPG.Character
{
    public class AIAttackState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.MovementComponent.StopMovingAgent();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.DistanceFromPlayer > enemy.AttackRange)
            {
                enemy.SwitchState(enemy.ChaseState);
                return;
            }

            enemy.CombatComponent.StartAttack();
        }
    }
}
