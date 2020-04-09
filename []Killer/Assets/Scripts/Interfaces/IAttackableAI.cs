using UnityEngine;

public interface IAttackableAI
{
    float attackRadius { get; set; }
    float attacksPerSecond { get; set; }
    float nextTimeToAttack { get; set; }
    Player target { get; set; }

    void FaceTarget();
    void AttackTarget();
}
