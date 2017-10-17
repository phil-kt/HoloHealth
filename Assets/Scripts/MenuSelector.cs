using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class MenuSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnSelect() {
        var hit = HoloToolkit.Unity.InputModule.GazeManager.Instance.HitInfo;
        if (hit.transform.gameObject == null) {
            return;
        }
        var gameObjectName = hit.transform.gameObject.name;
        print(gameObjectName);
        var selectText = GameObject.Find("SelectText");
        selectText.GetComponent<TextMesh>().text = gameObjectName;
    }
}
