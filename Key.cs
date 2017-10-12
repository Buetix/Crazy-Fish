using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    private GameController theGameController;
	// Use this for initialization
	void Start () {
        theGameController = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        KeyMovement();
	}
    void KeyMovement()
    {
        Vector3 temp = transform.position;
        temp.x -= theGameController.keyMoveSpeed * Time.deltaTime;
        transform.position = temp;
    }
}
