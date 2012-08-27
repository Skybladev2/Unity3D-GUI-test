using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Sprites/Sample Sprite")]
[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
public class SampleSprite : MonoBehaviour
{
	[SerializeField]
	private Vector2 size = Vector2.one;
	[SerializeField]
	private Vector2 zero = Vector2.one / 2;
	[SerializeField]
	private Rect textureCoords = Rect.MinMaxRect (0, 0, 1, 1);
	[SerializeField]
	private bool pixelCorrect = true;
	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;
	
	private static Mesh CreateMesh (Vector2 size, Vector2 zero, Rect textureCoords)
	{
		var vertices = new[]
	                       {
	                         new Vector3 (0, 0, 0),          // 1 ___  2
	                         new Vector3 (0, size.y, 0),     //   |  |
	                         new Vector3 (size.x, size.y, 0),//   |  |
	                         new Vector3 (size.x, 0, 0)      // 0 ---- 3
	                       };
	
		Vector3 shift = Vector2.Scale (zero, size);
		for (int i = 0; i < vertices.Length; i++) {
			vertices [i] -= shift;
		}
	
		var uv = new[]
	        {
	          new Vector2 (textureCoords.xMin, 1 - textureCoords.yMax),
	          new Vector2 (textureCoords.xMin, 1 - textureCoords.yMin),
	          new Vector2 (textureCoords.xMax, 1 - textureCoords.yMin),
	          new Vector2 (textureCoords.xMax, 1 - textureCoords.yMax)
	        };
	
		var triangles = new[]
	      {
	        0, 1, 2,
	        0, 2, 3
	      };
	
		return new Mesh { vertices = vertices, uv = uv, triangles = triangles };
	}
	
  	#region Unity messages

	// Use this for initialization
	private void Awake ()
	{
		meshFilter = GetComponent<MeshFilter> ();
		meshRenderer = GetComponent<MeshRenderer> ();
	}

	private void Start ()
	{
		// NOTE: initializing mesh here because our camera is initialized in Awake()
		InitializeMesh ();
	}

  	#endregion
	
	private void InitializeMesh ()
	{
		Camera cam = Camera.main;
		if (pixelCorrect && cam != null) {
			float ratio = cam.pixelHeight / (2 * cam.orthographicSize);
			size.x = NonNormalizedTextureCoords.width * ratio;
			size.y = NonNormalizedTextureCoords.height * ratio;
		}
		
		meshFilter.mesh = CreateMesh (size, zero, textureCoords);
	}
	
	private Rect NonNormalizedTextureCoords {
		get {
			Rect coords = textureCoords;
			Vector2 texSize = TextureSize;
			if (texSize != Vector2.zero) {
				coords.xMin *= texSize.x;
				coords.xMax *= texSize.x;
				coords.yMin *= texSize.y;
				coords.yMax *= texSize.y;
			}
			return coords;
		}
	}
	
	private Vector2 TextureSize {
		get {
			if (meshRenderer == null)
				return Vector2.zero;
			Material mat = meshRenderer.sharedMaterial;
			if (mat == null)
				return Vector2.zero;
			Texture tex = mat.mainTexture;
			if (tex == null)
				return Vector2.zero;
			return new Vector2 (tex.width, tex.height);
		}
	}
}