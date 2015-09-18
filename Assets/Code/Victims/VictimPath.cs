using UnityEngine;
using System.Collections;

public class VictimPath : MonoBehaviour {
	PathNode[] PathNodes;

	// Use this for initialization
	void Start () {
		PathNodes = GetComponentsInChildren<PathNode>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos() {
		PathNode[] Nodes = GetComponentsInChildren<PathNode>();

		if (Nodes.Length > 0) {
			Gizmos.color = Color.magenta;

			Vector3 LastPos = Nodes[0].transform.position;
			for (int i = 1; i < Nodes.Length; i++) {
				Gizmos.DrawLine (LastPos, Nodes [i].transform.position);
				LastPos = Nodes [i].transform.position;
			}
		}
	}
}
