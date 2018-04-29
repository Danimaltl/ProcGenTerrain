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

    [Range(4, 200)]
    public int length = 50, width = 50;

    [Range(.01f, 2)]
    public float offset;

    [Range(0,20)]
    public float noiseAmt = 0;
    [Range(.1f, 20)]
    public float rangex, rangey = 10;
    [Range(0, 20)]
    public float intensity = 10;

    // Use this for initialization
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();


    }

    // Update is called once per frame
    void Update()
    {
        mc.Clear();
        CreatePlane(length, width);
    }

    void CreatePlane(int length, int width)
    {
        List<Vector3> vertices = new List<Vector3>();
        //Generate grid of vertices
        for (int x = 0; x <= length; x++)
        {
            for (int z = 0; z <= width; z++)
            {
                Vector3 noiseVert = new Vector3(((float) x)/rangex, ((float) z)/rangey, noiseAmt);
                vertices.Add(new Vector3(x * offset,Perlin.Noise(noiseVert)*intensity,z * offset));
            }
        } 

        //Build Squares
        //Iterate through all but first x vert, all bust last y vert
        for (int x = 1; x <= length; x++)
        {
            for (int z = 0; z < length; z++)
            {
                mc.BuildTriangle(vertices[x * (width + 1) + (z + 1)], vertices[x * (width + 1) + z], vertices[(x - 1) * (width + 1) + (z + 1)]);
                mc.BuildTriangle(vertices[x*(width+1)+z], vertices[(x-1) * (width + 1) + z], vertices[(x-1) * (width + 1) + (z+1)]);
            }
        }

        meshFilter.mesh = mc.CreateMesh();
    }
}
