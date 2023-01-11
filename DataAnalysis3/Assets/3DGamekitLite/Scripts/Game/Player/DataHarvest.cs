using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit3D;

public class DataHarvest : MonoBehaviour
{
    [SerializeField] private string url = "https://citmalumnes.upc.es/~jordiea3/Player.php";
    
    // Start is called before the first frame update

    private void OnEnable()
    {
        PlayerController.onPlayerDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        PlayerController.onPlayerDeath -= OnPlayerDeath;
    }

    private void OnPlayerDeath(Vector3 position)
    {
        WWWForm form = new WWWForm();
        form.AddField("posX", (int)position.x);
        form.AddField("posY", (int)position.y);
        form.AddField("posZ", (int)position.z);

        StartCoroutine(SendData(form)); 
    }

    private IEnumerator SendData(WWWForm form)
    {
        WWW www = new WWW(url, form);
        yield return www;

        if(!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            yield break;
        }
    }
}
