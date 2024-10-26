using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PrefabControl : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SaveToPrefab();
        }
    }

    public void SaveToPrefab()
    {
        var indentifier = new System.DateTimeOffset(System.DateTime.Now).ToUnixTimeSeconds();

        var meshFilter = this.gameObject.GetComponent<MeshFilter>();
        AssetDatabase.CreateAsset(meshFilter.sharedMesh, "Assets/Resources/SimulationInstances/MeshFilters/meshFilter_" + indentifier + ".asset");
        AssetDatabase.SaveAssets();


        GameObject simulationBox = GameObject.Find("3DSimulationBox_Test");

        simulationBox.transform.localScale = new Vector3(0.22f, 0.22f, 0.22f);

        simulationBox.GetComponentInChildren<SimulationController>().enabled = false;
        var lines = this.transform.GetComponentsInChildren<LineRenderer>();

        foreach(LineRenderer line in lines)
        {
            line.endWidth = 0.005f;
            line.startWidth = 0.005f;
        }

        PrefabUtility.SaveAsPrefabAsset(GameObject.Find("3DSimulationBox_Test"), "Assets/Resources/SimulationInstances/particleSetting_" + indentifier + ".prefab");
    }
}
