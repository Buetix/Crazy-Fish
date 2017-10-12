using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private GameController theGameController;

	void Start () {
        theGameController = FindObjectOfType<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
       EnemyMovement();
	}

    void EnemyMovement()
    {
        //theGameController.enemyMoveSpeed += level * 0.2f; 
        Vector3 temp = transform.position;
        
        temp.x -= theGameController.enemyMoveSpeed * Time.deltaTime;
        temp.y += Mathf.Sin(Time.time) *Time.deltaTime;
        transform.position = temp;
    }
}
