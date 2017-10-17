using System;
using System.Text;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
 

public class CredentialManager : MonoBehaviour {
	public static string token;
	public static readonly string clientID = "6j2esrvp4fzrt2nhhjzs98sc";
	public static readonly string secret = "u3Mrh2FCB2TwFZD";	
	static DateTime last;

	public class TokenClassName
	{
		public string access_token;
	}
	public static IEnumerator getCredentials() {
		if (!isTokenExpired ()) {
			yield return null;
		}
		string url = "https://api.athenahealth.com/oauthpreview/token";

		Dictionary<string, string> content = new Dictionary<string, string> ();
		//Fill key and value
		content.Add ("grant_type", "client_credentials");
		content.Add ("client_id", clientID);
		content.Add ("client_secret", secret);
	
		UnityWebRequest www = UnityWebRequest.Post (url, content);

		yield return www.Send();

		if (www.responseCode == 200)
		{
			string resultContent = www.downloadHandler.text;
			Debug.Log (resultContent);
			TokenClassName json = JsonUtility.FromJson<TokenClassName>(resultContent);

			//Return result
			token = json.access_token;
		}
		else
		{
			Debug.Log("Network Error" + www.responseCode);
		}
	}
	public static Boolean isTokenExpired() {
		if (last.Second == 0) {
			last = DateTime.Now;
			return true;
		} else {
			DateTime now = DateTime.Now;
			TimeSpan ts = now.Subtract (last);
			if (ts.Seconds > 3600) {
				return true;
			}
			return false;
		}
	}
}


