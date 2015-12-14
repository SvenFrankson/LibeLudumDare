using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Texture2D startButton;
	public GUISkin skin;
	private int phase = 0;

	private float timer;
	public float gameTime;
	public RandomFlowerGenerator rfg;
	public LibelluleControler lib;
	public SmoothCam cam;

	public void Start () {
		this.EndCurrentGame ();
	}

	public void StartGame () {
		this.GetComponent<MeshRenderer> ().enabled = false;
		foreach (Renderer r in this.GetComponentsInChildren<MeshRenderer> ()) {
			r.enabled = false;
		}
		timer = 0f;
		phase = 1;
		cam.usePosTargetMenu = false;
		rfg.Run ();
		lib.Run ();
	}

	public void EndCurrentGame () {
		this.GetComponent<MeshRenderer> ().enabled = true;
		foreach (Renderer r in this.GetComponentsInChildren<MeshRenderer> ()) {
			r.enabled = true;
		}
		phase = 0;
		rfg.Over ();
		lib.Over ();
		cam.usePosTargetMenu = true;
		foreach (Coin c in FindObjectsOfType<Coin> ()) {
			Destroy (c.gameObject);
		}
	}

	private int timeToEnd;
	public void OnGUI () {
		GUI.skin = this.skin;
		if (phase == 0) {

		}
		if (phase == 1) {
			if (timeToEnd <= 10) {
				if ((timer - Mathf.FloorToInt (timer)) < 0.3f) {
					GUILayout.BeginArea (new Rect (0f, 0f, Screen.width, Screen.height));
					GUILayout.FlexibleSpace ();
					GUILayout.BeginHorizontal ();
					GUILayout.FlexibleSpace ();
					GUILayout.Button (timeToEnd + "");
					GUILayout.FlexibleSpace ();
					GUILayout.EndHorizontal ();
					GUILayout.FlexibleSpace ();
					GUILayout.EndArea ();
				}
			}
			else {
				GUILayout.BeginArea (new Rect (3f * Screen.width / 4f, 0f, Screen.width / 4f, Screen.height / 4f));
				GUILayout.Label (timeToEnd + " s");
				GUILayout.EndArea ();
			}
		}
	}

	public void Update () {
		timer += Time.deltaTime;
		timeToEnd = Mathf.FloorToInt (gameTime - timer);
		if (timeToEnd < 0) {
			EndCurrentGame ();
		}
	}

	public void OnMouseDown () {
		this.StartGame ();
	}

	public void OnMouseOver () {
		this.GetComponent<Animator> ().SetTrigger ("Move");
	}
}
