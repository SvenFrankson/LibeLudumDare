using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class RandomColor : MonoBehaviour {

	void Start () {
		Renderer r = this.GetComponent<Renderer> ();

		for (int i = 0; i < r.materials.Length; i++) {
			r.materials[i].color = RandomColorManager.Manager.GetRandomColor ();
		}
	}
}
