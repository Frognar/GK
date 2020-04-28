using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(IMoveVelocity))]
public class MoveAround : MonoBehaviour
{
    private float movementTimeRemaining;
    private IMoveVelocity moveVelocity;
    private IPathfinding pathfinding;

    private void Start()
    {
        movementTimeRemaining = 0;
        moveVelocity = GetComponent<IMoveVelocity>();
        pathfinding = GetComponent<IPathfinding>();
    }

    void Rotate(float directionX, float directionZ)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionX, 0, directionZ));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void Update()
    {
        moveVelocity.SetDirection(Vector3.zero);

        if (movementTimeRemaining <= 0)
        {
            System.Random rnd = new System.Random((int)(transform.position.x + transform.position.y + transform.position.z + Time.time));
            int fate = rnd.Next(0, 100);
            if (fate > 10 & fate < 35)
            {
                Rotate(rnd.Next(-10, 10), rnd.Next(-10, 10));
                return;
            }

            if (fate > 60 & fate < 90)
            {
                movementTimeRemaining = rnd.Next(2, 4);
                return;
            }
        }
        else
        {
            if (pathfinding.GetDirBlocked())
                Rotate(transform.right.x, transform.right.z);
            else
            {
                Vector3 direction = transform.forward.normalized;
                direction.y = 0f;
                moveVelocity.SetDirection(direction);
                movementTimeRemaining -= Time.deltaTime;
            }
        }
    }
}
