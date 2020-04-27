using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAround : MonoBehaviour
{
    [SerializeField] float movementSpeed = 50f;
    private float movementTimeRemaining;
    private Rigidbody rigidBody;

    private void Start()
    {
        movementTimeRemaining = 0;
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Move()
    {
        Vector3 direction = transform.forward;
        direction.y = 0f;
        rigidBody.AddForce(direction * movementSpeed);

        movementTimeRemaining -= Time.deltaTime;
    }

    void Rotate(float directionX, float directionZ)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionX, 0, directionZ));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void Update()
    {
        if (gameObject.GetComponent<Enemy>().Unmobilized == true)
        {
            //Debug.Log("Daj mi się ruszyć");
            return;
        }

        if (movementTimeRemaining <= 0)
        {
            System.Random rnd = new System.Random((int)(transform.position.x + transform.position.y + transform.position.z + Time.time));
            int fate = rnd.Next(0, 100);

            if (fate > 10)
            {
                if (fate < 35)
                {
                    Rotate(rnd.Next(-10, 10), rnd.Next(-10, 10));
                    return;
                }

                if (fate > 60)
                {
                    if (fate < 90)
                    {
                        movementTimeRemaining = rnd.Next(2, 4);
                        return;
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (movementTimeRemaining > 0)
            Move();
    }
}
