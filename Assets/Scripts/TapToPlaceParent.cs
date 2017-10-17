using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;
using HoloToolkit.Unity.InputModule;

public class TapToPlaceParent : MonoBehaviour, IInputClickHandler
{
    bool placing = false;

    public void OnInputClicked(InputClickedEventData eventData)
    {

       
        if (!placing)
        {

            //this.transform.parent.localScale = new Vector3(1.0f, 10.0f, 1.0f);

        }
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        // On each Select gesture, toggle whether the user is in placing mode.
        placing = !placing;
        
        // If the user is in placing mode, display the spatial mapping mesh.
        if (placing)
        {
            SpatialMappingManager.Instance.DrawVisualMeshes = true;
            //this.transform.parent.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        


        }
        // If the user is not in placing mode, hide the spatial mapping mesh.
        else
        {
            PopulateDescription pop = new PopulateDescription();
            print(this.gameObject.name);
            pop.populateObject(this.gameObject);
            SpatialMappingManager.Instance.DrawVisualMeshes = false;

            
            this.transform.parent.localScale = new Vector3(1.0f, 10.0f, 1.0f);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the user is in placing mode,
        // update the placement to match the user's gaze.

        if (placing)
        {
            // Do a raycast into the world that will only hit the Spatial Mapping mesh.
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
			if (Physics.Raycast (headPosition, gazeDirection, out hitInfo,
				             30.0f, SpatialMappingManager.Instance.LayerMask)) {
				// Move this object's parent object to
				// where the raycast hit the Spatial Mapping mesh.
				if (hitInfo.distance > 5.0f) {
					this.transform.parent.position = headPosition + gazeDirection * 3.0f;
				} else {
					this.transform.parent.position = hitInfo.point;
				}
					
				// Rotate this object's parent object to face the user.
				Quaternion toQuat = Camera.main.transform.localRotation;
				toQuat.x = 0;
				toQuat.z = 0;
				this.transform.parent.rotation = toQuat;

			} else {
				this.transform.parent.position = headPosition + gazeDirection * 3.0f;
				Quaternion toQuat = Camera.main.transform.localRotation;
				toQuat.x = 0;
				toQuat.z = 0;
				this.transform.parent.rotation = toQuat;
            }
        }
    }
}