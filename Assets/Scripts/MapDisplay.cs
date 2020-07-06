using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public GameObject grass;

    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawTexture(Texture2D texture) {        
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture) {
		meshRenderer.sharedMaterial = (Material)Resources.Load("Material/MeshMat", typeof(Material));
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }

    public void DrawGrass(MeshData meshData, Texture2D texture, MeshData grassColourMeshData, Texture2D grassColourTexture) {
        DrawMesh(meshData, texture);
		meshRenderer.sharedMaterial = (Material)Resources.Load("Material/FieldTexture", typeof(Material));
        GrassGenerator.GenerateGrass(meshData, texture, grassColourMeshData, grassColourTexture);
    }

    public void RemoveGrass() {
        GrassGenerator.RemoveGrass();
    }
}
