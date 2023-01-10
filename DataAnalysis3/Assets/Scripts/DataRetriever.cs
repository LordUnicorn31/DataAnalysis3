using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class DataRetriever : MonoBehaviour
{
    private string data;
    public Vector3 positions;
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
            DataConverter(data);
        }
    }

    private void StartAnalysis()
    {
        var state = new Dictionary<string, object>();
        state["deaths"] = data;

        var result = Analytics.CustomEvent("data",state);
        Debug.Log("Analysis: " + result.ToString());
    }

    private void DataConverter(string dat)
    {
        var sStrings = dat.Split("\"");
        List<int> ints = new List<int>();
        List<Vector3> positions = new List<Vector3>();

        foreach (string s in sStrings)
        {
            if(int.TryParse(s,out int val))
            {
                ints.Add(val);
            }
        }

        //foreach (int i in ints)
        //{
        //    Debug.Log(i.ToString());

        //}

        for (int j = 1; j <= ints.Count; ++j)
        {
            Vector3 position = new Vector3((float)ints[j], (float)ints[j+1], (float)ints[j+2]);
            if (j % 3 == 0 || j == 1)
            {
                positions.Add(position);
                Debug.Log(position.ToString());
            }
        }

        //float x = float.Parse(posStrings[0]);
        //float y = float.Parse(posStrings[1]);
        //float z = float.Parse(posStrings[2]);

        //positions = new Vector3(x, y, z);
        //Debug.Log(positions.ToString());
    }

}
