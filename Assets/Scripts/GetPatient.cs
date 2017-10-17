using System;
using System.Text;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GetPatient : MonoBehaviour {

	// Use this for initialization
	public class PatientInfo
	{
		public string email;
		public string firstname;
		public string lastname;
		public string dob;
		public string homephone;

		public string ToString() {
			return 
			"Email: " + email+ "\n" + 
			"Name: " + firstname + " " + lastname + "\n" +
            "Date of Birth: " + dob + "\n" +
            "Phone: " + homephone;
		}
	}
    void Start()
    {
        getFromAPI(this.gameObject);
    }
    public IEnumerator getFromAPI (GameObject g_obj) {
        //var render = GameObject.Find ("apiTest").GetComponent<TextMesh> ();
        var render = g_obj.GetComponent<TextMesh>();
        var debugObj = GameObject.FindWithTag("DebugText");
        debugObj.GetComponent<TextMesh>().text = render.text;

        yield return CredentialManager.getCredentials ();
		string tok = CredentialManager.token;

		string url = "https://api.athenahealth.com/preview1/195900/patients/30621";


		Dictionary<string, string> content = new Dictionary<string, string> ();
		content.Add (CredentialManager.clientID, CredentialManager.secret);
	
		UnityWebRequest www = UnityWebRequest.Get (url);
		www.SetRequestHeader ("Authorization", "Bearer " + tok);
		yield return www.Send ();

		if (www.responseCode == 200) {
			print ("WWW Ok!: " + www.downloadHandler.text);
			String resultContent = www.downloadHandler.text;
            resultContent = resultContent.Substring(1, resultContent.Length - 2);
			PatientInfo json = JsonUtility.FromJson<PatientInfo>(resultContent);
			render.text = json.ToString();

			//render.text = "No crash?";
		} else {
			print ("WWW Error: " + www.responseCode);
			print (www.downloadHandler.text);
			render.text = www.downloadHandler.text;
			//render.text = "ok something broke";
		}    
	}


	
	// Update is called once per frame
//	void Update () {
//		
//	}
}
