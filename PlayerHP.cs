using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {

    public float currentHP;
    public float totalHP;
    // Use this for initialization
    void Start () {
        currentHP = totalHP;
	}
	
	// Update is called once per frame
	void Update () {
        MissKey();
	}

    void MissKey()
    {
        transform.localScale = new Vector3(currentHP / totalHP, 1, 1);
    }
}
