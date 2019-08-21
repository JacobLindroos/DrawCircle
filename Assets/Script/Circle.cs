using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour
{
	public int vertexCount = 40; //hur många punkter cirkeln ska bestå av
	public float lineWidth = 0.4f; //breden på linjen som ritar cirkeln
	public float radius; 
	public bool circleFillScreen;

	private LineRenderer lineRenderer;

	private void Awake()    //getcomponent tar mkt energi och är därför alltid smartast att initiera i awake
	{
		lineRenderer = GetComponent<LineRenderer>(); //hämtar line renderer component to the gameobject
		SetupCircle();
	}

	private void SetupCircle()
	{
		lineRenderer.widthMultiplier = lineWidth;

		if (circleFillScreen)
		{
			radius = Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMax, 0f)),
			Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMin, 0f))) * 0.5f - lineWidth;
		}

		float deltaTheta = (2f * Mathf.PI) / vertexCount;
		float theta = 0f;

		lineRenderer.positionCount = vertexCount;
		for (int i = 0; i < lineRenderer.positionCount; i++)
		{
			Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
			lineRenderer.SetPosition(i, pos);
			theta += deltaTheta;
		}

	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		float deltaTheta = (2f * Mathf.PI) / 40f; //graderna mellan de olika punkterna som utgör circkeln
		float theta = 0f;

		Vector3 startPos = Vector3.zero; //starting position of the circle
		for(int i = 0; i < vertexCount + 1; i++) 
		{
			Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f); // positions the circle is built up by
			Gizmos.DrawLine(startPos, transform.position + pos); //draws the circle from start position to end position + 1
			startPos = transform.position + pos;

			theta += deltaTheta;
		}

	}
#endif
}

