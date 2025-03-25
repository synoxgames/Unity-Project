using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour {

	private static Text popupText;
	private static PopupText instance;

	private void Awake() {
		popupText = GetComponent<Text>();

		if (instance)
			instance = null;
		instance = this;
    }

	public static void SetText(string message) {
		popupText.text = message;
    }

	public static void SetColour(Color toSet) {
		popupText.color = toSet;
    }

	public static void DelayTrigger(float amount) {
		instance.StopAllCoroutines();
		instance.StartCoroutine(Delay(amount));
    }

	private static IEnumerator Delay(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		ClearText();
    }

	public static void ClearText() {
		popupText.text = string.Empty;
    }
}
