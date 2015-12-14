using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Coin : MonoBehaviour {

	public float delay;
	public float lifetime;
	public float rotationSpeed;
	public float ascendSpeed;

	private TextMesh[] timer;

	public void Start () {
		this.ascendSpeed += Random.Range (-2f, 2f);

		timer = this.GetComponentsInChildren<TextMesh> ();
	}

	public void Update () {
		this.transform.RotateAround (this.transform.position, this.transform.up, rotationSpeed * Time.deltaTime);
		this.transform.position += ascendSpeed * Time.deltaTime * Vector3.up;
		
		delay += Time.deltaTime;

		foreach (TextMesh tm in this.timer) {
			tm.text = Mathf.FloorToInt (lifetime - delay) + "";
		}

		if (delay > lifetime) {
			Destroy (this.gameObject);
		}
	}
}
