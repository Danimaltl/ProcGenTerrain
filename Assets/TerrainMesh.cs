using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class TerrainMesh : MonoBehaviour
{
    MeshFilter meshFilter;
    Mesh mesh;
    MeshCreator mc = new MeshCreator();

    public Vector3 size = Vector3.one;
    public float offset = 1.2f;
    float noiseAmt = 0;

    // Use this for initialization
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();


    }

    // Update is called once per frame
    void Update()
    {
        mc.Clear();
        CreatePlane(50, 50);
        noiseAmt += .02f;
    }

    void CreatePlane(int length, int width)
    {
        List<Vector3> vertices = new List<Vector3>();
        //Generate grid of vertices
        for (int x = 0; x <= length; x++)
        {
            for (int z = 0; z <= width; z++)
            {
                vertices.Add(new Vector3(x,0,z));
            }
        } 

        //Build Squares
        //Iterate through all but first x vert, all bust last y vert
        for (int x = 1; x <= length; x++)
        {
            for (int z = 0; z < length; z++)
            {
                mc.BuildTriangle(vertices[x * (width + 1) + z], vertices[x * (width + 1) + (z + 1)], vertices[(x - 1) * (width + 1) + (z + 1)]);
                mc.BuildTriangle(vertices[x*(width+1)+z], vertices[(x-1) * (width + 1) + z], vertices[(x-1) * (width + 1) + (z+1)]);
            }
        }

        meshFilter.mesh = mc.CreateMesh();
    }
}
