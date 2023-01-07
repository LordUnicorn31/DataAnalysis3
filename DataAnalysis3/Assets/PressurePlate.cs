using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool lvlChange = false;
    public string url;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendPressurePlate()
    {
        bool lvlChange = true;
    }

    public WWWForm ProcessLvlData()
    {
        WWWForm form = new WWWForm();
        form.AddField("level", 1);

        return form;
    }

    public IEnumerator SendData(WWWForm form)
    {
        WWW www = new WWW(url, form);
        yield return www;

        if(!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
    }

    public void OnPress()
    {
        StartCoroutine(SendData(ProcessLvlData()));
    }
}
