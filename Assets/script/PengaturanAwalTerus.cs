using UnityEngine;
using System.Collections;

public class PengaturanAwalTerus : MonoBehaviour {
	public static PengaturanAwalTerus obyek = null;

	void Awake(){
		if (obyek == null)
			obyek = this;
		else if (obyek != null)
			Destroy (gameObject);

		DontDestroyOnLoad (this.gameObject);
	}
}
