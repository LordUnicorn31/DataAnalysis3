using UnityEngine;
using System.Collections;

public class DataRetriever : MonoBehaviour
{

    void Start()
    {
        // Send a request to the PHP script
        StartCoroutine(ImportData());
    }

    IEnumerator ImportData()
    {
        string url = "https://citmalumnes.upc.es/~jordiea3/data.php"; // Replace with the URL of the PHP script
        WWW www = new WWW(url);
        yield return www;

        if (www.error != null)
        {
            Debug.LogError(www.error);
        }
        else
        {
            string data = www.text;
            // Do something with the data, for example, you can parse it as JSON using SimpleJSON or other libraries
            Debug.Log(data);
        }
    }
}
