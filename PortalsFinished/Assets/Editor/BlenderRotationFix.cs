using System;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BlenderAssetProcessor : AssetPostprocessor
{
    public void OnPostprocessModel(GameObject obj)
    {
        ModelImporter importer = (ModelImporter) assetImporter;
        if (Path.GetExtension(importer.assetPath) == ".blend")
        {
            RotateObject(obj.transform);
        }

        obj.transform.rotation = Quaternion.identity;
    }

    private static void RotateObject(Transform obj)
    {
        // Минус x - чтобы компенсировать импортер по умолчанию, который разворачивает ось Х
        obj.position = new Vector3(-obj.position.x, obj.position.z, obj.position.y);

        // Если сам объект повернут (Rotation == [-90,0,0]) - поворачиваем его обратно на 90 градусов
        if (Math.Abs(obj.localRotation.eulerAngles.x) > .1f)
        {
        	obj.RotateAround(Vector3.zero, Vector3.right, 90);
        }

        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
        if (meshFilter != null) RotateMesh(meshFilter.sharedMesh);

        foreach (Transform child in obj) RotateObject(child);
    }

    private static void RotateMesh(Mesh mesh)
    {
        Vector3[] vertices = mesh.vertices;

        for (int index = 0; index < vertices.Length; index++)
        {
            // Минус x - чтобы компенсировать импортер по умолчанию, который разворачивает ось Х
            vertices[index] = new Vector3(-vertices[index].x, vertices[index].z, vertices[index].y);
        }

        mesh.vertices = vertices;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
