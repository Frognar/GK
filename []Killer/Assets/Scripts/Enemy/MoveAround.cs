using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    float movementSpeed;
    float movementTimeRemaining;

    private void Start()
    {
        movementSpeed = 5f;
        movementTimeRemaining = 0;
    }

    void Move()
    {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;

        movementTimeRemaining -= Time.deltaTime;
    }

    void Rotate(float directionX, float directionZ)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionX, 0, directionZ));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (movementTimeRemaining > 0)
        {
            Move();
        }
        else
        {
            System.Random rnd = new System.Random((int)(transform.position.x + transform.position.y + transform.position.z + Time.time));
            int fate = rnd.Next(0, 100);

            if (fate > 30)
            {
                if (fate < 35)
                {
                    Rotate(rnd.Next(-10, 10), rnd.Next(-10, 10));
                    return;
                }
                if (fate > 60)
                {
                    if (fate < 65)
                    {
                        movementTimeRemaining = rnd.Next(2, 5);
                        return;
                    }
                }
            }
        }
    }
}
