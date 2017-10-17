using UnityEngine;

public class ItemSelected : MonoBehaviour
{
    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        var hit = HoloToolkit.Unity.InputModule.GazeManager.Instance.HitInfo;
        
        var gameObjectName = hit.transform.gameObject.transform.parent.name;
        //print(gameObjectName);
        var selectText = GameObject.Find("SelectText");
        //this.gameObject.name
        selectText.GetComponent<TextMesh>().text = gameObjectName;
    }
}