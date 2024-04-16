using System.Collections.Generic;
using UnityEngine;

public class TileRow : MonoBehaviour {

    public bool shifted;
    public List<GameObject> tiles;

    void Awake() {
        tiles = new List<GameObject>();
    }
}
