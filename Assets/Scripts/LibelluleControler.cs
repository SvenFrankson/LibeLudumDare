using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class LibelluleControler : MonoBehaviour {
	
	private bool upAndRunning;
	private int score;

	public float wingTurn = 5f;
	public float wingPower = 10f;
	private Rigidbody rbdy;
	private Rigidbody Rbdy {
		get {
			if (rbdy == null) {
				rbdy = this.GetComponent<Rigidbody> ();
			}
			return rbdy;
		}
	}
	private Animator anim;

	private float pow;
	private float right;

	public float windLimit;
	public float windSize;
	public float windMaxStrength;
	private float windFA;
	private float windFB;

	public Vector3 startPos;
	public GUISkin skin;

	void Start () {
		this.anim = this.GetComponent<Animator> ();

		windFA = windSize / windMaxStrength;
		windFB = - windFA * windLimit;
	}
	
	public void Run () {
		this.score = 0;
		this.upAndRunning = true;
		this.Rbdy.isKinematic = false;
	}
	
	public void Over () {
		this.upAndRunning = false;
		this.Rbdy.isKinematic = true;
		this.transform.position = startPos;
		this.transform.rotation = Quaternion.identity;
	}
	
	public void OnGUI () {
		GUI.skin = skin;
		GUILayout.BeginArea (new Rect (0f, 0f, Screen.width / 4f, Screen.height / 4f));
		GUILayout.FlexibleSpace ();
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
		GUILayout.Button ("Score " + score);
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		GUILayout.FlexibleSpace ();
		GUILayout.EndArea ();
	}
	
	public void FixedUpdate () {
		if (!upAndRunning) {
			return;
		}
		float tmpPow = 0f;
		float tmpRight = 0f;
		if (Input.GetKey (KeyCode.A)) {
			tmpPow += 0.5f;
			tmpRight -= 1f;
		}
		if (Input.GetKey (KeyCode.D)) {
			tmpPow += 0.5f;
			tmpRight += 1f;
		}

		this.pow = (9f * this.pow + tmpPow) / 10f;
		this.right = (9f * this.right + tmpRight) / 10f;

		this.Rbdy.AddForce (this.transform.up.normalized * this.pow * this.wingPower);
		this.Rbdy.AddForce (this.ComputeWind ());
		this.Rbdy.AddTorque (this.transform.right * this.pow * this.wingTurn);

		this.Rbdy.AddTorque (- this.transform.forward * this.right * this.wingTurn);
		this.Rbdy.AddTorque (this.transform.up * this.right * this.wingTurn);

		this.Rbdy.AddTorque (Vector3.Cross (this.transform.up, Vector3.up) * this.wingTurn);

		anim.SetFloat ("Right", Vector3.Dot (this.Rbdy.velocity, this.transform.right) / 40f);
		anim.SetFloat ("Speed", this.pow);

		anim.SetFloat ("Pitch", Vector3.Dot (- this.Rbdy.velocity, this.transform.up) / 40f);

	}

	public Vector3 ComputeWind () {
		Vector3 pos = this.transform.position;
		float dist = pos.magnitude;

		Vector3 windForce = Vector3.zero;
		if (dist > this.windLimit) {
			windForce = - pos.normalized * (windFA * dist + windFB);
		}
		return windForce;
	}

	public void OnTriggerEnter (Collider c) {
		Coin coin = c.GetComponent<Coin> ();
		if (coin != null) {
			Destroy (coin.gameObject);
			this.score += Mathf.FloorToInt (coin.lifetime - coin.delay);
			this.GetComponent<AudioSource> ().Play ();
		}
	}
}
