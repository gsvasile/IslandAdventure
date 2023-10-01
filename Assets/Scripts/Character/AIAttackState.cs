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
            if (enemy.Player == null)
            {
                enemy.CombatComponent.CancelAttack();
                return;
            }

            if (enemy.DistanceFromPlayer > enemy.AttackRange)
            {
                enemy.CombatComponent.CancelAttack();
                enemy.SwitchState(enemy.ChaseState);
                return;
            }

            enemy.CombatComponent.StartAttack();
            enemy.transform.LookAt(enemy.Player.transform);
        }
    }
}
