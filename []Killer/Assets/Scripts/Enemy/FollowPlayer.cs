using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author:          Sebastian Przyszlak / Anna Mach (kod został przez Annę w większości przekopiowany z kodu Sebastiana (AttackingMoveAround),
 *  żeby nie dodawać tam warunku, który byłby spełniony tylko dla jednego przeciwnika)
 * Collaborators:   
 */

public class FollowPlayer : MonoBehaviour
{
    private EnemyAttack enemyAttack;
    private IMove move;
    private float nextTimeToAttack = 0f;
    private float angularSpeed = 10f;

    private void Start()
    {
        enemyAttack = GetComponent<EnemyAttack>();
        move = GetComponent<IMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = enemyAttack.TargetPosition - this.transform.position;
        float distance = direction.magnitude;

        move.SetDirectionVector(Vector3.zero);

        Rotate(direction.x, direction.z);
        if (distance <= enemyAttack.AttackRadius)
        {
            if (Time.time >= nextTimeToAttack)
            {
                enemyAttack.AttackTarget();
                nextTimeToAttack = Time.time + enemyAttack.AttackRate;
            }
        }
        else
        {
            direction.Normalize();
            direction.y = 0f;
            move.SetDirectionVector(direction);
        }
    }

    void Rotate(float xRotation, float zRotation)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(xRotation, 0, zRotation));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * angularSpeed);
    }

}
