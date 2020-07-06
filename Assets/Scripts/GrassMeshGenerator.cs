using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GrassMeshGenerator {
    public static GrassMeshData GenerateGrassMesh(MeshData mesh, int vindex, float heightModifier, float heightIncreaseModifier, float leaningModifier, float leaningIncreaseModifier, float width) {
        GrassMeshData meshData = new GrassMeshData(7);

        Vector3 position = new Vector3(0f,0f,0f);

		float uvX = 0f;
		float uvY = 0f;
		float uvYFirstClimb = 0.33f;
		float uvYSecondClimb = 0.66f;
		float uvYThirdClimb = 1.0f;


        int vertexIndex = 0;

        meshData.vertices[vertexIndex] = new Vector3(position.x, position.y, position.z);
        meshData.uvs[vertexIndex] = new Vector2(uvX, uvY);
        vertexIndex++;
        meshData.vertices[vertexIndex] = new Vector3(position.x + width, position.y, position.z);
        meshData.uvs[vertexIndex] = new Vector2(uvX, uvY);


        vertexIndex++;
        float bending = position.x + leaningModifier;
        float height = position.y + heightModifier;

        meshData.vertices[vertexIndex] = new Vector3(bending, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvX, uvYFirstClimb);
        vertexIndex++;
        meshData.vertices[vertexIndex] = new Vector3(bending + width/1.5f, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvX, uvYFirstClimb);

		
        meshData.AddTriangle(vertexIndex - 3, vertexIndex - 2, vertexIndex - 1);
        meshData.AddTriangle(vertexIndex - 1, vertexIndex - 2, vertexIndex);
        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 3, vertexIndex - 1);
        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 1, vertexIndex);
		

        vertexIndex++;
        bending = position.x + leaningModifier * leaningIncreaseModifier;
        height = position.y + heightModifier + heightModifier * heightIncreaseModifier;

        meshData.vertices[vertexIndex] = new Vector3(bending, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvX, uvYSecondClimb);
        vertexIndex++;
        meshData.vertices[vertexIndex] = new Vector3(bending + width/2f, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvX, uvYSecondClimb);


        meshData.AddTriangle(vertexIndex - 3, vertexIndex - 2, vertexIndex - 1);
        meshData.AddTriangle(vertexIndex - 1, vertexIndex - 2, vertexIndex);
        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 3, vertexIndex - 1);
        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 1, vertexIndex);

        
        vertexIndex++;
        bending = position.x + leaningModifier * leaningIncreaseModifier * leaningIncreaseModifier;
        height = position.y + heightModifier + heightModifier + heightModifier * heightIncreaseModifier * heightIncreaseModifier;

        meshData.vertices[vertexIndex] = new Vector3(bending, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvX, uvYThirdClimb);

        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 1, vertexIndex);
        meshData.AddTriangle(vertexIndex - 1, vertexIndex - 2, vertexIndex);

        return meshData;
    }
}

public class GrassMeshData {
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;

    public GrassMeshData(int numVertices) {
        vertices = new Vector3[numVertices];
        uvs = new Vector2[numVertices];
        triangles = new int[(numVertices - 2) * 6];
    }

    public void AddTriangle(int a, int b, int c) {
        triangles[triangleIndex] = a;
        triangles[triangleIndex+1] = b;
        triangles[triangleIndex+2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}

/*
    public static GrassMeshData GenerateGrassMesh(MeshData mesh, int vindex, float heightModifier, float heightIncreaseModifier, float leaningModifier, float leaningIncreaseModifier, float width) {
        GrassMeshData meshData = new GrassMeshData(7);

        Vector3 position = new Vector3(0f,0f,0f);

		float uvStartPosition = Random.Range(0.2f, 0.8f);
		float uvXClimbing = 0.005f;
		float uvYClimbing = 0.005f;


        int vertexIndex = 0;

        meshData.vertices[vertexIndex] = new Vector3(position.x, position.y, position.z);
        meshData.uvs[vertexIndex] = new Vector2(uvStartPosition, uvStartPosition);
        vertexIndex++;
        meshData.vertices[vertexIndex] = new Vector3(position.x + width, position.y, position.z);
        meshData.uvs[vertexIndex] = new Vector2(uvStartPosition + uvXClimbing, uvStartPosition);


        vertexIndex++;
        float bending = position.x + leaningModifier;
        float height = position.y + heightModifier;

        meshData.vertices[vertexIndex] = new Vector3(bending, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvStartPosition, uvStartPosition + uvYClimbing);
        vertexIndex++;
        meshData.vertices[vertexIndex] = new Vector3(bending + width/1.5f, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvStartPosition + uvXClimbing, uvStartPosition + uvYClimbing);

		
        meshData.AddTriangle(vertexIndex - 3, vertexIndex - 2, vertexIndex - 1);
        meshData.AddTriangle(vertexIndex - 1, vertexIndex - 2, vertexIndex);
        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 3, vertexIndex - 1);
        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 1, vertexIndex);
		

        vertexIndex++;
        bending = position.x + leaningModifier * leaningIncreaseModifier;
        height = position.y + heightModifier + heightModifier * heightIncreaseModifier;

        meshData.vertices[vertexIndex] = new Vector3(bending, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvStartPosition, uvStartPosition + 2 * uvYClimbing);
        vertexIndex++;
        meshData.vertices[vertexIndex] = new Vector3(bending + width/2f, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvStartPosition + uvXClimbing, uvStartPosition + 2 * uvYClimbing);


        meshData.AddTriangle(vertexIndex - 3, vertexIndex - 2, vertexIndex - 1);
        meshData.AddTriangle(vertexIndex - 1, vertexIndex - 2, vertexIndex);
        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 3, vertexIndex - 1);
        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 1, vertexIndex);

        
        vertexIndex++;
        bending = position.x + leaningModifier * leaningIncreaseModifier * leaningIncreaseModifier;
        height = position.y + heightModifier + heightModifier + heightModifier * heightIncreaseModifier * heightIncreaseModifier;

        meshData.vertices[vertexIndex] = new Vector3(bending, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvStartPosition + uvXClimbing, uvStartPosition + 3 * uvYClimbing);

        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 1, vertexIndex);
        meshData.AddTriangle(vertexIndex - 1, vertexIndex - 2, vertexIndex);

        return meshData;
    }
}*/

/*
public static GrassMeshData GenerateGrassMesh(MeshData mesh, int vindex, float heightModifier, float heightIncreaseModifier, float leaningModifier, float leaningIncreaseModifier, float width) {
        GrassMeshData meshData = new GrassMeshData(7);

        Vector3 position = new Vector3(0f,0f,0f);

		float uvXTerrain = mesh.uvs[vindex].x;
		float uvYTerrain = mesh.uvs[vindex].y;


        int vertexIndex = 0;

        meshData.vertices[vertexIndex] = new Vector3(position.x, position.y, position.z);
        meshData.uvs[vertexIndex] = new Vector2(uvXTerrain, uvYTerrain);
        vertexIndex++;
        meshData.vertices[vertexIndex] = new Vector3(position.x + width, position.y, position.z);
        meshData.uvs[vertexIndex] = new Vector2(uvXTerrain, uvYTerrain);


        vertexIndex++;
        float bending = position.x + leaningModifier;
        float height = position.y + heightModifier;

        meshData.vertices[vertexIndex] = new Vector3(bending, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvXTerrain, uvYTerrain);
        vertexIndex++;
        meshData.vertices[vertexIndex] = new Vector3(bending + width/1.5f, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvXTerrain, uvYTerrain);

		
        meshData.AddTriangle(vertexIndex - 3, vertexIndex - 2, vertexIndex - 1);
        meshData.AddTriangle(vertexIndex - 1, vertexIndex - 2, vertexIndex);
        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 3, vertexIndex - 1);
        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 1, vertexIndex);
		

        vertexIndex++;
        bending = position.x + leaningModifier * leaningIncreaseModifier;
        height = position.y + heightModifier + heightModifier * heightIncreaseModifier;

        meshData.vertices[vertexIndex] = new Vector3(bending, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvXTerrain, uvYTerrain);
        vertexIndex++;
        meshData.vertices[vertexIndex] = new Vector3(bending + width/2f, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvXTerrain, uvYTerrain);


        meshData.AddTriangle(vertexIndex - 3, vertexIndex - 2, vertexIndex - 1);
        meshData.AddTriangle(vertexIndex - 1, vertexIndex - 2, vertexIndex);
        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 3, vertexIndex - 1);
        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 1, vertexIndex);

        
        vertexIndex++;
        bending = position.x + leaningModifier * leaningIncreaseModifier * leaningIncreaseModifier;
        height = position.y + heightModifier + heightModifier + heightModifier * heightIncreaseModifier * heightIncreaseModifier;

        meshData.vertices[vertexIndex] = new Vector3(bending, height, position.z);
		meshData.uvs[vertexIndex] = new Vector2(uvXTerrain, uvYTerrain);

        meshData.AddTriangle(vertexIndex - 2, vertexIndex - 1, vertexIndex);
        meshData.AddTriangle(vertexIndex - 1, vertexIndex - 2, vertexIndex);

        return meshData;
    }
	*/