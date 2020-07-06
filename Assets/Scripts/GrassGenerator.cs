using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGenerator : MonoBehaviour {

    public static void GenerateGrass(MeshData meshData, Texture2D texture, MeshData grassColourMeshData, Texture2D grassColourTexture) {

        Vector3[] vertices = meshData.vertices;
		Vector2[] uvs = meshData.uvs;
		Vector2[] uvsColour = grassColourMeshData.uvs;

		float grassRotationBias = 50f;

		//GameObject MapGenerator = GameObject.Find("MapGenerator");
		//Color standard = MapGenerator.GetComponent<MapGenerator>().regions[2].colour;

		for (int i = 0; i < 5; i++) {
			for (int v = 0; v < vertices.Length; v++) {
				GrassMeshData grassMeshData = GrassMeshGenerator.GenerateGrassMesh(meshData, v, 0.4f, 0.8f, 0.2f, 2.0f, 0.25f);
				
				GameObject grass = new GameObject("GrassBlade");

				grass.transform.position = RandomizePosition(vertices[v]);

				Vector3 euler = grass.transform.eulerAngles;


				euler.y = RandomizeRotation(grassRotationBias);


				grass.transform.eulerAngles = euler;
				grass.transform.parent = GameObject.Find("GrassContainer").transform;

				grass.AddComponent<MeshFilter>();
				grass.AddComponent<MeshRenderer>();
				grass.GetComponent<MeshFilter>().mesh = grassMeshData.CreateMesh();
				MeshRenderer rend = grass.GetComponent<MeshRenderer>();

				Material fieldMaterial = (Material)Resources.Load("Material/FieldTexture", typeof(Material));

				rend.material = fieldMaterial;
				rend.material.mainTexture = SimulateLighting(fieldMaterial, uvs, v);

				Color standard = texture.GetPixelBilinear(uvs[v].x, uvs[v].y);
				Color colourMapPixel = grassColourTexture.GetPixelBilinear(uvsColour[v].x, uvsColour[v].y);
				rend.material.color = RandomizeColor(standard, colourMapPixel);

				//Yellow
				// 169 188 34
				/*
				green 96 128 56




				*/
			}
		}
    } 

	private static Texture2D SimulateLighting(Material mat, Vector2[] uvs, int v) {
		int height = 64;
		int width = 64;

		Texture2D texture = new Texture2D(width, height);
		Texture2D fieldTexture = (Texture2D) mat.mainTexture;
		Color color = fieldTexture.GetPixelBilinear(uvs[v].x, uvs[v].y);

		Color[] colorArray = texture.GetPixels();

		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				colorArray[y * width + x] = lightingEffectFunc(height, y) * color;
			}
		}

		texture.SetPixels(colorArray);
		texture.Apply();

		return texture;
	}

	private static float lightingEffectFunc(float height, float curr) {
		return curr/height * 1.5f;
	}

	private static Vector3 RandomizePosition(Vector3 vertex) {
		Vector3 position = new Vector3(Random.Range(vertex.x - 1f, vertex.x + 1f), vertex.y, Random.Range(vertex.z - 1f, vertex.z + 1f));
		return position;
	}

	private static float RandomizeRotation(float direction) {
		float newRotation = direction * NextGaussianDouble();
		return newRotation;
	}

	private static Color RandomizeColor(Color standard, Color color) {
		float maxR = Mathf.Max(standard.r, color.r);
		float maxB = Mathf.Max(standard.b, color.b);
		float maxG = Mathf.Max(standard.g, color.g);

		float minR = Mathf.Min(standard.r, color.r);
		float minB = Mathf.Min(standard.b, color.b);
		float minG = Mathf.Min(standard.g, color.g);

		Color newColor = new Color(Random.Range(minR, maxR), Random.Range(minG, maxG), Random.Range(minB, maxB), 0);
		return newColor;
	}

	private static float NextGaussianDouble() {
		float u, v, S;

		do
		{
			u = 2.0f * Random.value - 1.0f;
			v = 2.0f * Random.value - 1.0f;
			S = u * u + v * v;
		}
		while (S >= 1.0f);

		float fac = Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
		return u * fac;
	}

    public static void RemoveGrass() {

        Transform grassContainer = GameObject.Find("GrassContainer").transform;

        while(grassContainer.childCount != 0){
            DestroyImmediate(grassContainer.GetChild(0).gameObject);
        }
    }
}