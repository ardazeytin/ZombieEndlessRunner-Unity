using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnim : MonoBehaviour {

	void Update ()
    {
        transform.Rotate(Vector3.up * 60 * Time.deltaTime);	
	}
}
