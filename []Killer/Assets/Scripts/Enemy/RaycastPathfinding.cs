using UnityEngine;

public class RaycastPathfinding : MonoBehaviour, IPathfinding
{
    [SerializeField] float raycastDistance = 10f;
    [SerializeField] int rayCount = 5;

    private bool dirBlocked = false;

    public bool GetDirBlocked()
    {
        return dirBlocked;
    }

    void Update()
    {
        Pathfinding();
    }

    void Pathfinding()
    {
        dirBlocked = false;

        for(float i = -5; i <= 5; i += 10f / (rayCount - 1))
        {
            Ray ray = new Ray(transform.position - transform.right * i - transform.up * 4, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (hit.transform == this.transform)
                    continue;
                dirBlocked = true;
            }
        }

    }

    private void OnDrawGizmos()
    {
        for (float i = -5; i <= 5; i += 10f / (rayCount - 1))
        {
            Ray left = new Ray(transform.position - transform.right * i - transform.up * 4, transform.forward);
            Debug.DrawRay(left.origin, left.direction * raycastDistance);
        }
    }
}
