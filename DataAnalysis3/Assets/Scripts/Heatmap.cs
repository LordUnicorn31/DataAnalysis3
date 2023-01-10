using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heatmap : MonoBehaviour
{
    public GameObject cubePrefab;
    public Material heatmapMaterial;
    private List<Vector3> deathPositions;
    // Start is called before the first frame update
    void Start()
    {
        deathPositions = new List<Vector3>();
        //deathPositions.Add(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        HeatMap();
    }

    private void HeatMap()
    {
        foreach(Vector3 position in deathPositions)
        {
            GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
            cube.GetComponent<Renderer>().material = heatmapMaterial;
        }
    }

    public void RemoveCubes()
    {
        Destroy(this.gameObject);
    }
}
