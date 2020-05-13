using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
public interface IMove {
    void SetDirectionVector (Vector3 directionVector);
    void Jump ();
}