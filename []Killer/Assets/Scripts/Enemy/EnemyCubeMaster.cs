using System.Collections.Generic;
using UnityEngine;

public class EnemyCubeMaster : Enemy
{
    public BoxCollider mainCollider;
    public Collider masterCollider;
    private List<Vector3> cubesPosition = new List<Vector3> {
        new Vector3( 5f, 0f, -5f),  // LEFT
        new Vector3( -5f, 0f, -5f), // RIGHT
        new Vector3( 0f, 0f, -10f), // BACK
        new Vector3( 0f, 5f, -5f),  // UP
        new Vector3( 0f, -5f, -5f), // DOWN
    };
    private List<Vector3> cubesRotation = new List<Vector3> {
        new Vector3(0f,90f,0f),     // LEFT
        new Vector3(0f,-90f,0f),    // RIGHT
        new Vector3(0f,180f,0f),    // BACK
        new Vector3(-90f,0f,0f),    // UP
        new Vector3(90f,0f,0f),     // DOWN
    };
    [SerializeField] private List<Enemy> enemySquad = new List<Enemy>();

    public void DestroyCube () {
        //squares no longer belong to cube master
        foreach (Enemy enemy in enemySquad)
            resetEnemy (enemy);

        enemySquad.Clear ();

        mainCollider.center = new Vector3 (0f, 0f, 0f);
        mainCollider.size = new Vector3 (10f, 10f, 0.05f);
    }

    void setEnemy (Enemy enemy, int index) {
        enemy.GetComponent<MoveAround> ().enabled = false;
        enemy.GetComponent<BoxCollider> ().enabled = false;
        Rigidbody rb = enemy.GetComponent<Rigidbody> ();
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.interpolation = RigidbodyInterpolation.None;

        //gameObject is now EnemyCubeMaster child and move with him
        enemy.transform.parent = this.transform;

        //Set rotation to parent rotation
        enemy.transform.rotation = this.transform.rotation;
        //rotate by cubesRotation[index]
        enemy.transform.rotation *= Quaternion.Euler (cubesRotation[index]);
        enemy.transform.localPosition = cubesPosition[index];
    }

    void resetEnemy (Enemy enemy) {
        enemy.transform.parent = this.transform.parent;
        enemy.transform.rotation = this.transform.rotation;

        if(enemySquad.Count > 4)
            if (enemy.Equals (enemySquad[4]))
                enemy.transform.position = transform.position + new Vector3 (0f, 0f, 1f);
        if(enemySquad.Count > 3)
            if (enemy.Equals (enemySquad[3]))
                enemy.transform.position = transform.position + new Vector3 (0f, 0f, -1f);

        enemy.GetComponent<MoveAround> ().enabled = true;
        enemy.GetComponent<BoxCollider> ().enabled = true;
        Rigidbody rb = enemy.GetComponent<Rigidbody> ();
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void OnTriggerEnter (Collider other) {
        if (health < maxHealth) {
            //Disable collider so we stop checking it when EnemyCubeMaster is hurt
            //(EnemyCubeMaster still have second collider)
            masterCollider.enabled = false;
            return;
        }

        //We don't take AttackingEnemy or EnemyCubeMaster to cube
        if (other.CompareTag ("Enemy") && enemySquad.Count < 5) {
            Debug.Log ("Dotknął mnie!");
            Enemy otherEnemy = other.GetComponent<Enemy> ();
            setEnemy (otherEnemy, enemySquad.Count);
            enemySquad.Add (otherEnemy);
            mainCollider.center = new Vector3 (0f, 0f, -5f);
            mainCollider.size = new Vector3 (10f, 10f, 10f);
        }
    }
}
