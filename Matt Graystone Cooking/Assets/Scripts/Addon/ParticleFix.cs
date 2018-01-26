using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Renderer>().sortingLayerName = "Particle";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
