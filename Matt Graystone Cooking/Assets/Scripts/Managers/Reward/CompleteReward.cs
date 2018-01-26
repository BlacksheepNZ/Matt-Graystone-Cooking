using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteReward : MonoBehaviour {

    public Button ButtonOk;

    public Text Text;

	// Update is called once per frame
	public void Hide ()
    {
        this.gameObject.SetActive(false);
	}

    public void Show(string text)
    {
        Text.text = text;
        this.gameObject.SetActive(true);
    }
}
