using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransformVelocity : MonoBehaviour, IMoveVelocity
{
    [SerializeField] private float movementSpeed = 25f;

    private Vector3 directionVector;

    public void SetDirection(Vector3 directionVector)
    {
        this.directionVector = directionVector;
    }

    public void Update()
    {
        transform.position += directionVector * movementSpeed * Time.deltaTime;
    }

}
