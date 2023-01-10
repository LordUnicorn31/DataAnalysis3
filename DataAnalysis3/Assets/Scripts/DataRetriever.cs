using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class DataRetriever : MonoBehaviour
{
    private string data;
    void Start()
    {
        // Send a request to the PHP script
        StartCoroutine(ImportData());
        StartAnalysis();
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
            data = www.text;
            Debug.Log(data);
        }
    }

    private void StartAnalysis()
    {
        var state = new Dictionary<string, object>();
        state["deaths"] = data;

        var result = Analytics.CustomEvent("data",state);
        Debug.Log("Analysis: " + result.ToString());
    }

}
