using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class DataRetriever : MonoBehaviour
{
    public class Cube
    {
        public Vector3 position;
        public int times = 0;

        //Constructor
        public Cube(Vector3 position)
        {
            this.position = position;
            this.times = 0;
        }
    }

    private string data;
    public List<Cube> cubes;
    public GameObject CubeParent;
    [Header("Cube creation")]
    public GameObject cubePrefab;
    public Material heatmapMaterial;
    public int maxDensity = 20;
    public int maxDistance = 2;
    

    private void Awake()
    {
        cubes = new List<Cube>();
    }
    void Start()
    {
        StartCoroutine(StartAnalysis());
    }

    private IEnumerator ImportData()
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

    private IEnumerator StartAnalysis()
    {
        yield return ImportData();

        PlotAnalysis();
    }


    private void DataConverter(string dat)
    {
        var sStrings = dat.Split("\"");
        List<int> ints = new List<int>();

        foreach (string s in sStrings)
        {
            if(int.TryParse(s,out int val))
            {
                ints.Add(val);
            }
        }

        for (int j = 0; j < ints.Count; j+=3)
        {
            Vector3 position = new Vector3((float)ints[j], (float)ints[j+1], (float)ints[j+2]);

            bool isNear = false;

            for (int i = 0; i < cubes.Count; ++i)
            {
                if (cubes[i].position == position) continue;

                if(Vector3.Distance(cubes[i].position,position) < maxDistance)
                {
                    isNear = true;
                    cubes[i].times++;
                    break;
                }
            }
            if(!isNear)
            {
                cubes.Add(new Cube(position));
                Debug.Log(position.ToString());
            }
            
        } 

    }

    private void HeatMap()
    {
        foreach (Cube cube  in cubes)
        {
            GameObject c = Instantiate(cubePrefab, cube.position, Quaternion.identity);
            c.transform.SetParent(CubeParent.transform);
            Renderer render = c.GetComponent<Renderer>();
            float ratio = (float)cube.times / maxDensity;
            if (ratio > 1) ratio = 1;
            ratio = (ratio - 1) * -1;
            Color cubeColor = new Color(1, ratio, ratio, 1);
            render.material.color = cubeColor;
        }
    }

    public void RemoveCubes()
    {
        Destroy(this.gameObject);
    }

    public void PlotAnalysis()
    {
        HeatMap();
    }

}
