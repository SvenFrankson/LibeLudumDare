using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomFlowerGenerator : MonoBehaviour {

	private bool upAndRunning;

	public int radius;
	public int distanceFlowers;
	public int distanceWoods;
	public float rateFlowers;
	public float rateWoods;

	private GameObject[] prefabsFlowers;
	private GameObject[] PrefabsFlowers {
		get {
			if (prefabsFlowers == null) {
				prefabsFlowers = Resources.LoadAll<GameObject> ("Prefabs/Flowers");
			}
			return prefabsFlowers;
		}
	}

	private GameObject[] prefabsWoods;
	private GameObject[] PrefabsWoods {
		get {
			if (prefabsWoods == null) {
				prefabsWoods = Resources.LoadAll<GameObject> ("Prefabs/Woods");
			}
			return prefabsWoods;
		}
	}

	private List<GameObject> flowers;
	public GameObject targetPrefab;
	public float targetPrefabRate;
	public float targetPrefabHeightZero;

	public void Start () {
		this.flowers = new List<GameObject> ();
		this.Generate ();
	}

	public void Run () {
		this.upAndRunning = true;
	}

	public void Over () {
		this.upAndRunning = false;
	}

	private float delay = 0f;
	public void Update () {
		if (this.upAndRunning) {
			delay += Time.deltaTime;
			if (delay > this.targetPrefabRate) {
				delay = 0f;
				this.RandomlyPopTarget ();
			}
		}
	}

	public void RandomlyPopTarget () {
		int index = Random.Range (0, flowers.Count - 1);
		GameObject target = GameObject.Instantiate<GameObject> (targetPrefab);
		target.transform.position = flowers [index].transform.position + Vector3.up * targetPrefabHeightZero;
	}

	public void Generate () {
		/*
		for (int i = - radius; i <= radius; i += distanceFlowers) {
			for (int j = - radius; j <= radius; j += distanceFlowers) {
				if (i * i + j * j < sqrRadius) {
					if (Random.Range (0f, 100f) < rateFlowers) {
						int index = Random.Range (0, PrefabsFlowers.Length - 1);
						GameObject g = GameObject.Instantiate (PrefabsFlowers [index]);
						g.transform.position = (i) * Vector3.right + (j) * Vector3.forward;
						//g.transform.position += RandomVector3 (1f, 0, 1f) * distanceFlowers / 8f ;
						g.transform.position += GroundGenerator.Ground.WorldHeight (g.transform.position) * Vector3.up;
						g.transform.rotation = Quaternion.Euler (0f, Random.Range (0f, 360f), 0f);
					}
				}
			}
		}
		*/

		for (int j = 0; j < GroundGenerator.Ground.map.height; j++) {
			for (int i = 0; i < GroundGenerator.Ground.map.width; i++) {
				float h = GroundGenerator.Ground.map.GetPixel (i, j).r;
				if (h > 0.1f) {
					if (Random.Range (0f, 100f) < rateFlowers) {
						int index = Random.Range (0, PrefabsFlowers.Length - 1);
						GameObject g = GameObject.Instantiate (PrefabsFlowers [index]);
						this.flowers.Add (g);
						g.transform.position = GroundGenerator.Ground.Offset + i * GroundGenerator.Ground.size * Vector3.right + j * GroundGenerator.Ground.size * Vector3.forward;
						//g.transform.position += RandomVector3 (1f, 0, 1f) * distanceFlowers / 8f ;
						g.transform.position += GroundGenerator.Ground.map.GetPixel (i, j).r * GroundGenerator.Ground.height * Vector3.up;
						g.transform.rotation = Quaternion.Euler (0f, Random.Range (0f, 360f), 0f);
					}
				}
			}
		}

		/*
		for (int i = - radius; i <= radius; i += distanceWoods) {
			for (int j = - radius; j <= radius; j += distanceWoods) {
				if (Random.Range (0f, 100f) < rateWoods) {
					if (i * i + j * j < sqrRadius) {
						int index = Random.Range (0, PrefabsWoods.Length - 1);
						GameObject g = GameObject.Instantiate (PrefabsWoods [index]);
						g.transform.position = (i) * Vector3.right + (j) * Vector3.forward;
						g.transform.position += RandomVector3 (1f, 0, 1f) * distanceWoods / 4f;
						g.transform.rotation = Quaternion.Euler (0f, Random.Range (0f, 360f), 0f);
					}
				}
			}
		}
		*/
	}

	static public Vector3 RandomVector3 (float x, float y, float z) {
		Vector3 r = Vector3.zero;
		r += Random.Range (-1f, 1f) * Vector3.right * x;
		r += Random.Range (-1f, 1f) * Vector3.up * y;
		r += Random.Range (-1f, 1f) * Vector3.forward * z;

		return r.normalized;
	}
}
