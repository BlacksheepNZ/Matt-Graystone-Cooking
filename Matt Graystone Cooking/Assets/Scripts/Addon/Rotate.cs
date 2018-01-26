using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float Speed = 1;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        float value = Speed * Time.deltaTime;

        this.transform.Rotate(new Vector3(0, 0, value));
    }
}
