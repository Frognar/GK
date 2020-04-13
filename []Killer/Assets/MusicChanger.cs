using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{

    private List<Collider> inCollider = new List<Collider>();

    private void LateUpdate()
    {
        if (inCollider.Count > 0 && !GameManager.inBattle)
            GameManager.inBattle = true;

        else if (inCollider.Count == 0 && GameManager.inBattle)
            GameManager.inBattle = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            inCollider.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        inCollider.Remove(other);
    }

}
