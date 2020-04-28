using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveVelocity : MonoBehaviour, IMoveVelocity
{
    [SerializeField] private float movementSpeed = 15f;

    private Vector3 directionVector;
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void SetDirection(Vector3 directionVector)
    {
        this.directionVector = directionVector;
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = directionVector * movementSpeed;
    }

}
