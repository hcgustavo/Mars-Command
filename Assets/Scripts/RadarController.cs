using UnityEngine;
using System.Collections;

public class RadarController : MonoBehaviour {
    public Transform radarDish;
    public float rotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        radarDish.Rotate(0.0f, 0.0f, rotationSpeed * Time.deltaTime);
	}
}
