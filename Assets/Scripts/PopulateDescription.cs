using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateDescription : MonoBehaviour {

    public void populateObject(GameObject g_obj)
    {
        if (g_obj.name.Equals("Heart Rate bg"))
        {
            GameObject patientText = GameObject.Find("PatientText");
            //patientText.SetActive(false);
            GetPatient gPatient = new GetPatient();
            gPatient.getFromAPI(patientText);
        }
    }
}
