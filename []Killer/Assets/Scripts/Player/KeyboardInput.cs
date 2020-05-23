using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
public class KeyboardInput : MonoBehaviour {
    private IMove move;

    private void OnDisable () {
        move.SetDirectionVector (Vector3.zero);
    }

    private void Awake () {
        move = GetComponent<IMove> ();
        if (move == null)
            Debug.LogError ("No movement script in Player!");
    }

    private void PlayerMovement () {
        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");

        Vector3 direction = transform.right * x + transform.forward * z;
        move.SetDirectionVector (direction);

        if (Input.GetKeyDown (KeyCode.Space)) {
            move.Jump ();
        }
    }

    private void Update () {
        PlayerMovement ();
        CheckForAbilities();
    }

    private void CheckForAbilities()
    {
        //czy da się tego gracza "zdobyć" jakoś inaczej?
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FindObjectOfType<Player>().UseAbility('1');
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FindObjectOfType<Player>().UseAbility('2');
        }
    }
}
