using UnityEngine;
using System.Collections;

public class RandomColorManager : MonoBehaviour {

	static public RandomColorManager manager;
	static public RandomColorManager Manager {
		get {
			if (RandomColorManager.manager == null) {
				RandomColorManager.manager = FindObjectOfType<RandomColorManager> ();
			}
			return RandomColorManager.manager;
		}
	}

	public Color[] colors;

	public Color GetRandomColor () {
		int index = Random.Range (0, this.colors.Length - 1);
		return colors [index];
	}
}
