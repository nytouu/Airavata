using UnityEngine;
using NaughtyAttributes;


[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), (typeof(MeshRenderer)))]
public class MeshFromHeightmap : MonoBehaviour
{
	private enum MeshResolution
	{
		VeryLow32 = 32,
		Low64 = 64,
		Medium128 = 128,
		High256 = 256,
		VeryHigh512 = 512
	};

	[ShowAssetPreview(256, 256)]
	[OnValueChanged(nameof(OnValueChanged))]
	[SerializeField] private Texture2D heightmap;

	[OnValueChanged(nameof(OnValueChanged))]
	[SerializeField] private Material material;

	[OnValueChanged(nameof(OnValueChanged))]
	[Range(10f, 200f)]
	[SerializeField] private float size = 100f;

	[OnValueChanged(nameof(OnValueChanged))]
	[Range(0f, 200f)]
	[SerializeField] private float intensity = 15f;

	[OnValueChanged(nameof(OnValueChanged))]
	[SerializeField] private MeshResolution resolution = MeshResolution.Medium128;

	[SerializeField] private bool autoMeshUpdateEnabled = false;

	[ShowNonSerializedField] private int _verticesCount = 0;
	[ShowNonSerializedField] private int _trianglesCount = 0;

	private Vector3[] _vertices;
	private Vector2[] _uvs;

	private MeshRenderer _meshRenderer;
	private MeshFilter _meshFilter;

	void Start()
	{
		_meshRenderer = GetComponent<MeshRenderer>();
		_meshFilter = GetComponent<MeshFilter>();

		RegenerateMesh();
	}

	[Button]
	[DisableIf(nameof(autoMeshUpdateEnabled))]
	public void RegenerateMesh()
	{
		_meshFilter.mesh = null;
		_meshFilter.sharedMesh = null;
		_meshRenderer.material = null;
		_meshRenderer.sharedMaterial = null;

		int width = (int)resolution;
		int depth = (int)resolution;

		_verticesCount = (width + 1) * (depth + 1);
		_trianglesCount = width * depth * 2 * 3;

		// Defining vertices
		_vertices = new Vector3[_verticesCount];
		_uvs = new Vector2[_verticesCount];

		int i = 0;
		for (int d = 0; d <= depth; d++)
		{
			for (int w = 0; w <= width; w++)
			{
				Vector3 position = new Vector3(
					(w - width / 2f) * size / width,
					GetHeightValue(heightmap, w, d) * intensity,
					(d - depth / 2f) * size / depth
				);
				_vertices[i] = position;
				_uvs[i] = new Vector2(w / width, d / depth);

				i++;
			}
		}

		// Defining triangles
		int[] triangles = new int[_trianglesCount];
		int vertex = 0;
		int triangle = 0;
		for (int d = 0; d < depth; d++)
		{
			for (int w = 0; w < width; w++)
			{
				// First triangle
				triangles[triangle + 0] = vertex;
				triangles[triangle + 1] = vertex + width + 1;
				triangles[triangle + 2] = vertex + 1;

				// Second triangle
				triangles[triangle + 3] = vertex + 1;
				triangles[triangle + 4] = vertex + width + 1;
				triangles[triangle + 5] = vertex + width + 2;
				triangle += 6;
				vertex++;
			}
			vertex++;
		}

		// Construct mesh
		Mesh mesh = new Mesh();
		mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

		mesh.SetVertices(_vertices);
		mesh.SetIndices(triangles, MeshTopology.Triangles, 0);
		mesh.SetUVs(0, _uvs);

		mesh.RecalculateNormals();
		mesh.Optimize();

		_meshFilter.mesh = mesh;
		_meshFilter.sharedMesh = mesh;
		_meshRenderer.material = material;
		_meshRenderer.sharedMaterial = material;
	}

	private float GetHeightValue(Texture2D texture, int x, int y)
	{
		int step = texture.width / (int)resolution;

		Color pixel = texture.GetPixel(x * step, y * step);
		return pixel.r;
	}

	private void OnValueChanged()
	{
		if (autoMeshUpdateEnabled)
			RegenerateMesh();
	}
}
