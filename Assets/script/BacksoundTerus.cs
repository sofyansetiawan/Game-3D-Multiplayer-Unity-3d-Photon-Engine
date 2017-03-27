using UnityEngine;
using System.Collections;

public class BacksoundTerus : MonoBehaviour {
	public static BacksoundTerus obyek = null;

	void Awake(){
		if (obyek == null)
			obyek = this;
		else if (obyek != null)
			Destroy (gameObject);

		DontDestroyOnLoad (this.gameObject);
	}
}
