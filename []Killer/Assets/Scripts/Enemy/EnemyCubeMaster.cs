using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCubeMaster : Enemy
{
    //Nie chciało mi się myśleć
    public Enemy enemyUp;
    public Enemy enemyDown;
    public Enemy enemyLeft;
    public Enemy enemyRight;
    public Enemy enemyBack;

    private bool weHaveFullSquad = false;

    private void Start()
    {
        enemyUp = null;
        enemyDown = null;
        enemyLeft = null;
        enemyRight = null;
        enemyBack = null;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(health < maxHealth)
        {
            //Możnaby tu jakoś wyłączyć collider, żeby w ogóle nie trzeba było tego sprawdzać
            return;
        }

        //Nie wciągamy AttackingEnemy ani EnemyCubeMaster
        if (other.tag == "Enemy")
        {
            Debug.Log("Dotknął mnie!");
            if(weHaveFullSquad == false)
            {
                if(enemyLeft == null)
                {
                    enemyLeft = other.GetComponent<Enemy>();
                    enemyLeft.Unmobilized = true;

                    //Obiekt zostaje dzieckiem CubeMaster i porusza się odtąd razem z nim
                    enemyLeft.transform.parent = gameObject.transform;

                    //Obrót bokiem do Mastera
                    Vector3 rot = enemyLeft.transform.localRotation.eulerAngles;
                    rot.y = - rot.y + 90;
                    rot.x = 0;
                    rot.z = 0;
                    enemyLeft.transform.Rotate(rot);

                    //enemyLeft.transform.position = new Vector3(0, 0, -(GetComponent<Collider>().bounds.size.x) / 2);

                }
                else if(enemyRight == null)
                {
                    enemyRight = other.GetComponent<Enemy>();
                    enemyRight.Unmobilized = true;
                    enemyRight.transform.parent = gameObject.transform;

                    //Obrót bokiem do Mastera
                    Vector3 rot = enemyRight.transform.localRotation.eulerAngles;
                    rot.y = - rot.y + 90;
                    rot.x = 0;
                    rot.z = 0;
                    enemyRight.transform.Rotate(rot);

                    //enemyRight.transform.position = new Vector3(0, 0, (GetComponent<Collider>().bounds.size.x) / 2);
                }
                else if (enemyBack == null)
                {
                    enemyBack = other.GetComponent<Enemy>();
                    enemyBack.Unmobilized = true;
                    enemyBack.transform.parent = gameObject.transform;

                    //Obrót równolegle do Mastera
                    Vector3 rot = enemyBack.transform.localRotation.eulerAngles;
                    rot.y = -rot.y;
                    rot.x = -rot.x;
                    rot.z = -rot.z;
                    enemyBack.transform.Rotate(rot);

                    //enemyBack.transform.position = new Vector3(GetComponent<Collider>().bounds.size.x, 0, 0);

                }
                else if (enemyUp == null)
                {
                    enemyUp = other.GetComponent<Enemy>();
                    enemyUp.Unmobilized = true;
                    enemyUp.transform.parent = gameObject.transform;

                    //Obrót równolegle do Mastera
                    Vector3 rot = enemyUp.transform.localRotation.eulerAngles;
                    rot.y = -rot.y;
                    rot.x = -rot.x;
                    rot.z = -rot.z + 90;
                    enemyUp.transform.Rotate(rot);

                    //enemyUp.transform.position = new Vector3(GetComponent<Collider>().bounds.size.x, 0, 0);
                }
                else if (enemyDown == null)
                {
                    enemyDown = other.GetComponent<Enemy>();
                    enemyDown.Unmobilized = true;
                    enemyDown.transform.parent = gameObject.transform;

                    //Obrót równolegle do Mastera
                    Vector3 rot = enemyDown.transform.localRotation.eulerAngles;
                    rot.y = -rot.y;
                    rot.x = -rot.x;
                    rot.z = -rot.z + 90;
                    enemyDown.transform.Rotate(rot);

                    //enemyDown.transform.position = new Vector3(GetComponent<Collider>().bounds.size.x, 0, 0);
                }

            }
        }
    }
}
