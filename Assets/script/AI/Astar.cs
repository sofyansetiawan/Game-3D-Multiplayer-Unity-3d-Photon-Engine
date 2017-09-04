using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class Astar : MonoBehaviour {

	Transform dicari;
	public Transform musuhNPC;
	public GameObject sistemGrid;
	public string tagPlayer = "Player";
	public int maxJarak = 10;

	public Stopwatch timer;

	Grid grid;

	void Awake() {
		timer = new Stopwatch();
		grid = sistemGrid.GetComponent<Grid>();
	}

	void Start(){
		
	}

	void Update() {
		if (GameObject.FindGameObjectWithTag(tagPlayer)) {
			dicari = GameObject.FindGameObjectWithTag (tagPlayer).GetComponent<Transform> ();
		}

		if (this.gameObject && dicari) {
			FindPath (dicari.position, musuhNPC.transform.position);
		}
	}

	void FindPath(Vector3 startPos, Vector3 targetPos) {
		timer.Reset();
		timer.Start();
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

		UnityEngine.Debug.Log ("Posisi Awal NPC: " + musuhNPC.transform.position);
		UnityEngine.Debug.Log ("Posisi Player: " + dicari.transform.position);

		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);

		while (openSet.Count > 0) {
			Node currentNode = openSet[0];
			for (int i = 1; i < openSet.Count; i ++) {
				if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost 
					&& openSet[i].hCost < currentNode.hCost) {
					currentNode = openSet[i];
				}
			}

			openSet.Remove(currentNode);
			closedSet.Add(currentNode);

			if (currentNode == targetNode) {
				RetracePath(startNode,targetNode);
				timer.Stop();
				UnityEngine.Debug.Log ("waktu (milidetik) : " +timer.ElapsedMilliseconds);
				UnityEngine.Debug.Log ("===BATAS==================================================================================================================");
				return;
			}

			foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
					continue;
				}

				int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
				if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newMovementCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = currentNode;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}

	void RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		Vector3 posisiNode = new Vector3(0.0f, 0.0f, 0.0f);

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
			UnityEngine.Debug.Log ("Node (Grid X: " +currentNode.gridX + ", Grid Y: " +currentNode.gridY);
			posisiNode = currentNode.worldPosition - new Vector3 (-0.5f, 0.1f, -0.5f);
			UnityEngine.Debug.Log ("Posisi Node:" +posisiNode);
		}
		path.Reverse();
		UnityEngine.Debug.Log ("Jumlah Path : " + path.Count);
		grid.path = path;
		MovementTarget (path);
	}

	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}

	void MovementTarget(List<Node> path){
		if (path.Count-1 != 0) {
			musuhNPC.position = Vector3.MoveTowards (musuhNPC.position
				, new Vector3 (path [0].worldPosition.x, musuhNPC.position.y, path [0].worldPosition.z)
				,maxJarak * Time.deltaTime);
		}
	}
}
