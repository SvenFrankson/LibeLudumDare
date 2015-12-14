using UnityEngine;
using System.Collections;

public class SmoothCam : MonoBehaviour {

	public bool usePosTargetMenu;
	public Transform posTarget;
	public Transform postTargetMenu;
	public Transform lookTarget;
	public float smoothNess;
	
	void FixedUpdate () {
		if (this.usePosTargetMenu) {
			this.transform.position = (this.transform.position * smoothNess + postTargetMenu.transform.position) / (smoothNess + 1f);
		}
		else {
			this.transform.position = (this.transform.position * smoothNess + posTarget.transform.position) / (smoothNess + 1f);
		}

		this.transform.LookAt (lookTarget);
	}
}
