using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

namespace CarVr
{
    public class CheckInternet : MonoBehaviour
    {
        public GameObject connection, noConnection;

        // Start is called before the first frame update
        void Start()
        {
            connection.SetActive(false);
            noConnection.SetActive(true);

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.Log("Network not available");
            }
            else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                Debug.Log("Network is available");
            }
            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                Debug.Log("Network is available through mobile data");
            }

            StartCoroutine(VerifyInternetConnection());
        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator VerifyInternetConnection()
        {
            UnityWebRequest www = new UnityWebRequest("https://google.com");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {www.error}");
                noConnection.SetActive(true);
            }
            else
            {
                Debug.Log($"Response: connection");
                connection.SetActive(true);
                noConnection.SetActive(false);
            }
        }
    }
}
