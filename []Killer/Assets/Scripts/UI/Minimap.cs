using UnityEngine;

/**
 * Author:          Sebastian Przyszlak
 * Collaborators:   
 */
public class Minimap : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = PlayerManager.instance.player.transform;
    }

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
