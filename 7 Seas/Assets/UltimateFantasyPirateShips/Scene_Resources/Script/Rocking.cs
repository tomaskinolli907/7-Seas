using UnityEngine;
using System.Collections;

public class Rocking :  MonoBehaviour
{
	private float time = 0;
	public float Intensity = 0.002f;
	public float angularspeed = 1f;
	public float FBIntensity = 2;

	public 	void Update ()
	{
		time += Time.fixedDeltaTime;
		transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y - Intensity * Mathf.Sin (time), transform.localPosition.z);
		Vector3 rotate = new Vector3 (transform.localRotation.x - (FBIntensity * Mathf.Sin (time) + 2), transform.localRotation.y, transform.localRotation.z +  (angularspeed * Mathf.Sin (time) + 2));
		Quaternion rotation = Quaternion.Euler (rotate);
		transform.localRotation = rotation;
	}

}
