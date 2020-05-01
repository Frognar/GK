using UnityEngine;

public class AttackingMoveAround : MoveAround {

    [SerializeField] private float lookRadius = 60f;
    private EnemyAttack enemyAttack;
    private float nextTimeToAttack = 0f;

    protected override void Awake () {
        base.Awake ();
        enemyAttack = GetComponent<EnemyAttack> ();
        if (enemyAttack == null)
            Debug.LogError ("No enemyAttack script on Attacking NPC [" + this.name + "]");
    }

    protected override void Update () {
        Vector3 direction = enemyAttack.TargetPosition - this.transform.position;
        float distance = direction.magnitude;

        move.SetMoveVector (Vector3.zero);

        if (distance <= lookRadius) {
            Rotate (direction.x, direction.z);
            if (distance <= enemyAttack.AttackRadius) {
                if (Time.time >= nextTimeToAttack) {
                    enemyAttack.AttackTarget ();
                    nextTimeToAttack = Time.time + enemyAttack.AttackRate;
                }
            } else {
                direction.Normalize ();
                direction.y = 0f;
                move.SetMoveVector (direction);
            }
        } else
            RandomMovement ();

        ControllHeight ();
    }

    public void OnDrawGizmosSelected () {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere (transform.position, lookRadius);
    }
}
