using UnityEngine;
using System.Collections;

public class LoadAwalTerus : MonoBehaviour {
	public static LoadAwalTerus obyek = null;

	void Awake(){
		if (obyek == null)
			obyek = this;
		else if (obyek != null)
			Destroy (gameObject);

		DontDestroyOnLoad (this.gameObject);
	}
}
