using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class GroundGenerator : MonoBehaviour {

	static public GroundGenerator ground;
	static public GroundGenerator Ground {
		get {
			if (GroundGenerator.ground == null) {
				GroundGenerator.ground = FindObjectOfType<GroundGenerator> ();
			}
			return GroundGenerator.ground;
		}
	}

	public float height;
	public float size;
	public Texture2D map;

	private MeshFilter mf;

	private Vector3 offset = Vector3.up;
	public Vector3 Offset {
		get {
			if (offset == Vector3.up) {
				offset = new Vector3 (- (map.width / 2) * size, 0f, - (map.height / 2) * size);
			}

			return offset;
		}
	}

	public void Start () {
		mf = this.GetComponent<MeshFilter> ();
		mf.mesh = this.GenerateMesh ();
		this.GetComponent<MeshCollider> ().sharedMesh = mf.mesh;
	}

	private Mesh GenerateMesh () {
		if (this.map == null) {
			return null;
		}

		Mesh m = new Mesh ();

		int w = map.width;
		int h = map.height;

		Vector3[] vertices = new Vector3 [w * h];
		for (int j = 0; j < h; j++) {
			for (int i = 0; i < w; i++) {
				vertices [i + j * w] = Offset + i * this.size * Vector3.right + j * this.size * Vector3.forward;
				vertices [i + j * w] += map.GetPixel (i, j).r * this.height * Vector3.up;
			}
		}

		int[] triangles = new int[6 * (w - 1) * (h - 1)];
		for (int j = 0; j < h - 1; j++) {
			for (int i = 0; i < w - 1; i++) {
				triangles [6 * j * (w - 1) + 6 * i] = j * w + i;
				triangles [6 * j * (w - 1) + 6 * i + 1] = (j + 1) * w + i;
				triangles [6 * j * (w - 1) + 6 * i + 2] = (j + 1) * w + (i + 1);
				
				triangles [6 * j * (w - 1) + 6 * i + 3] = j * w + i;
				triangles [6 * j * (w - 1) + 6 * i + 4] = (j + 1) * w + (i + 1);
				triangles [6 * j * (w - 1) + 6 * i + 5] = j * w + (i + 1);
			}
		}

		m.vertices = vertices;
		m.triangles = triangles;
		m.RecalculateNormals ();

		return m;
	}
}
