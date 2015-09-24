using UnityEngine;
using System.Collections;

public class MapParentHandle : MonoBehaviour {
	public static MapParentHandle instance;
	// Use this for initialization
	void Start () {
		MapParentHandle.instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
