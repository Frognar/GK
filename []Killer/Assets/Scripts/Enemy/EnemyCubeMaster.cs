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

                    //cholibka, to jeszcze ogarnę obrót względem mastera i przesunięcie, teraz nie mam siły
                    Vector3 rot = transform.localRotation.eulerAngles;
                    //Vector3 oldRot = enemyLeft.transform.localRotation.eulerAngles;
                    rot.y = 90;//rot.y + oldRot.y + 90;
                    rot.x = 0;
                    rot.z = 0;

                    enemyLeft.transform.Rotate(rot);
                }
                else if(enemyRight == null)
                {
                    enemyRight = other.GetComponent<Enemy>();
                    enemyRight.Unmobilized = true;
                    enemyRight.transform.parent = gameObject.transform;
                }
                else if (enemyBack == null)
                {
                    enemyBack = other.GetComponent<Enemy>();
                    enemyBack.Unmobilized = true;
                    enemyBack.transform.parent = gameObject.transform;


                }
            }
        }
    }
}
