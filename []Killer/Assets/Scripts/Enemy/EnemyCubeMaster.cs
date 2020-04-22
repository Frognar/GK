using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCubeMaster : Enemy
{
    private float cubeSize = 1f;

    public Enemy enemyUp;
    public Enemy enemyDown;
    public Enemy enemyLeft;
    public Enemy enemyRight;
    public Enemy enemyBack;

    private bool weHaveFullSquad = false;

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        soundManager = SoundManager.instance;

        enemyUp = null;
        enemyDown = null;
        enemyLeft = null;
        enemyRight = null;
        enemyBack = null;
        
    }

    public void DestroyCube()
    {
        //Kwadraty przestają należeć do cube
        if (enemyLeft != null)
        {
            enemyLeft.transform.parent = gameObject.transform.parent;
            enemyLeft.GetComponent<NavMeshAgent>().enabled = true;
            enemyLeft.Unmobilized = false;
            enemyLeft = null;

            if (enemyRight != null)
            {
                enemyRight.transform.parent = gameObject.transform.parent;
                enemyRight.GetComponent<NavMeshAgent>().enabled = true;
                enemyRight.Unmobilized = false;
                enemyRight = null;

                if (enemyBack != null)
                {
                    enemyBack.transform.parent = gameObject.transform.parent;
                    enemyBack.GetComponent<NavMeshAgent>().enabled = true;
                    enemyBack.Unmobilized = false;
                    enemyBack = null;

                    if (enemyUp != null)
                    {
                        enemyUp.transform.parent = gameObject.transform.parent;
                        enemyUp.GetComponent<NavMeshAgent>().enabled = true;
                        enemyUp.Unmobilized = false;
                        enemyUp = null;

                        if (enemyDown != null)
                        {
                            enemyDown.transform.parent = gameObject.transform.parent;
                            enemyDown.GetComponent<NavMeshAgent>().enabled = true;
                            enemyDown.Unmobilized = false;
                            enemyDown = null;
                        }
                    }
                }
            }
        }
    }

    /*
    private void Update()
    {
        if (health < maxHealth)
            destroyCube();
    }
    */
    void OnTriggerEnter(Collider other)
    {
        if(health < maxHealth)
        {
            //Disable collider so we stop checking it when EnemyCubeMaster is hurt
            //(EnemyCubeMaster still have second collider)
            other.enabled = false;
            return;
        }

        //We don't take AttackingEnemy or EnemyCubeMaster to cube
        if (other.tag == "Enemy")
        {
            Debug.Log("Dotknął mnie!");
            if(weHaveFullSquad == false)
            {
                if(enemyLeft == null)
                {
                    enemyLeft = other.GetComponent<Enemy>();
                    enemyLeft.Unmobilized = true;
                    //gameObject is now EnemyCubeMaster child and move with him
                    enemyLeft.transform.parent = gameObject.transform;
                    enemyLeft.GetComponent<NavMeshAgent>().enabled = false;

                    Vector3 rot = enemyLeft.transform.localRotation.eulerAngles;
                    rot.y = - rot.y + 90;
                    rot.x = 0;
                    rot.z = 0;
                    enemyLeft.transform.Rotate(rot);

                    enemyLeft.transform.localPosition = new Vector3(cubeSize / 2, 0, -cubeSize / 2);

                }
                else if(enemyRight == null)
                {
                    enemyRight = other.GetComponent<Enemy>();
                    enemyRight.Unmobilized = true;
                    enemyRight.transform.parent = gameObject.transform;
                    enemyRight.GetComponent<NavMeshAgent>().enabled = false;

                    Vector3 rot = enemyRight.transform.localRotation.eulerAngles;
                    rot.y = - rot.y + 90;
                    rot.x = 0;
                    rot.z = 0;
                    enemyRight.transform.Rotate(rot);

                    enemyRight.transform.localPosition = new Vector3(-cubeSize / 2, 0, -cubeSize / 2);
                }
                else if (enemyBack == null)
                {
                    enemyBack = other.GetComponent<Enemy>();
                    enemyBack.Unmobilized = true;
                    enemyBack.transform.parent = gameObject.transform;
                    enemyBack.GetComponent<NavMeshAgent>().enabled = false;

                    Vector3 rot = enemyBack.transform.localRotation.eulerAngles;
                    rot.y = -rot.y;
                    rot.x = -rot.x;
                    rot.z = -rot.z;
                    enemyBack.transform.Rotate(rot);

                    enemyBack.transform.localPosition = new Vector3(0, 0, -cubeSize);

                }
                else if (enemyUp == null)
                {
                    weHaveFullSquad = true;

                    enemyUp = other.GetComponent<Enemy>();
                    enemyUp.Unmobilized = true;
                    enemyUp.transform.parent = gameObject.transform;
                    enemyUp.GetComponent<NavMeshAgent>().enabled = false;

                    //Obrót równolegle do Mastera
                    Vector3 rot = enemyUp.transform.localRotation.eulerAngles;
                    rot.y = -rot.y;
                    rot.x = -rot.x;
                    rot.z = (-rot.z) + 90;
                    enemyUp.transform.Rotate(rot);

                    enemyUp.transform.localPosition = new Vector3(0, cubeSize , -cubeSize/2);
                }
                /*else if (enemyDown == null)
                {
                    weHaveFullSquad = true;

                    enemyDown = other.GetComponent<Enemy>();
                    enemyDown.Unmobilized = true;
                    enemyDown.transform.parent = gameObject.transform;

                    //Obrót równolegle do Mastera
                    Vector3 rot = enemyDown.transform.localRotation.eulerAngles;
                    rot.y = -rot.y;
                    rot.x = -rot.x;
                    rot.z = -rot.z + 90;
                    enemyDown.transform.Rotate(rot);

                    //enemyDown.transform.localPosition = new Vector3(GetComponent<Collider>().bounds.size.x, 0, 0);
                }*/

            }
        }
    }
}
