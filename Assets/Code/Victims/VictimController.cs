using UnityEngine;
using System.Collections;

public class VictimController : MonoBehaviour {
	public RectTransform _RectTransform;
	public VictimPath MyPath; // variable set by VictimsMainController
	[Range(1.0f, 1000.0f)]
	public float VictimSpeed = 10.0f;
	int NodeIdx;
	// Use this for initialization
	void Start () {
		NodeIdx = 0;
		transform.position = MyPath.PathNodes [NodeIdx].transform.position;
		NodeIdx ++;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, MyPath.PathNodes [NodeIdx].transform.position, Time.deltaTime * VictimSpeed);

		if (Vector3.Distance (transform.position, MyPath.PathNodes [NodeIdx].transform.position) <= 0.01f) {
			if (NodeIdx < MyPath.PathNodes.Length - 1) {
				NodeIdx ++;
			} else {
				// finished
			}
		}
	}
}
