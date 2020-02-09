using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class levelGeneration : MonoBehaviour
{

    //[SerializeField] private List<GameObject> _levelTiles;
    [SerializeField] private List<GameObject> _floorTiles;
    [SerializeField] private List<GameObject> _wallTiles;
    [SerializeField] private List<GameObject> _environmentals;
    [SerializeField] private TMP_InputField xDim;
    [SerializeField] private TMP_InputField yDim;
    [SerializeField] private Transform parentFolder;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateLevel()
    {
        //Destroy the level
        foreach (Transform child in parentFolder) {
            GameObject.Destroy(child.gameObject);
        }
        
        //Get editor values for dimensions of room
        int x = int.Parse(xDim.text);
        int y = int.Parse(yDim.text);

        int EAST_WALL = 0;
        int WEST_WALL = y-1;
        int NORTH_WALL = 0;
        int SOUTH_WALL = x-1;

        List<float> widths = new List<float>();
        List<float> lengths = new List<float>();
        
        foreach (GameObject tile in _floorTiles){
            widths.Add(tile.GetComponent<MeshRenderer>().bounds.size.z);
            lengths.Add(tile.GetComponent<MeshRenderer>().bounds.size.x);
        }

        GameObject[,] tiles = new GameObject[x,y];

        for (int i = 0; i < widths.Count; i++)
        {
            Debug.Log(widths[i] + " " + lengths[i]);
        }
        
        for (int i = 0; i < x; i++){
            for (int j = 0; j < y; j++) {
                //Create an x BY y array of tiles and place them in the correct distance
                tiles[i,j] = Instantiate(_floorTiles[Random.Range(0, _floorTiles.Count)]);
                tiles[i,j].transform.position = new Vector3(i*widths[0], 0, j*widths[0]);
                tiles[i,j].transform.SetParent(parentFolder);
                tiles[i, j].name = "floor_" + i + "_" + j;
                
                //Create horizontal & vertical walls
                if (i == NORTH_WALL) {
                    tiles[i,j] = Instantiate(_wallTiles[0]);
                    tiles[i,j].transform.position = new Vector3(i - widths[0]/2, 0, j*widths[0]);
                    tiles[i,j].transform.SetParent(parentFolder);
                    tiles[i, j].name = "north_wall_" + i + "_" + j;
                }
                else if (i == SOUTH_WALL) {
                    tiles[i,j] = Instantiate(_wallTiles[0]);
                    tiles[i,j].transform.position = new Vector3(i*widths[0] + widths[0]/2, 0, j*widths[0]);
                    tiles[i,j].transform.SetParent(parentFolder);
                    tiles[i, j].name = "south_wall_" + i + "_" + j;
                }
                else if (j == EAST_WALL) {
                    tiles[i,j] = Instantiate(_wallTiles[0]);
                    tiles[i,j].transform.position = new Vector3(i*widths[0], 0, j*1.15f - widths[0]/2);
                    tiles[i,j].transform.Rotate(0,90,0);
                    tiles[i, j].name = "east_wall_" + i + "_" + j;
                    tiles[i,j].transform.SetParent(parentFolder);
                }
                else if (j == WEST_WALL) {
                    tiles[i,j] = Instantiate(_wallTiles[0]);
                    tiles[i,j].transform.position = new Vector3(i*widths[0], 0, j*widths[0] + widths[0]/2);
                    tiles[i,j].transform.Rotate(0,90,0);
                    tiles[i, j].name = "west_wall_" + i + "_" + j;
                    tiles[i,j].transform.SetParent(parentFolder);
                }
                
                //Create corners
                if (i == NORTH_WALL && j == EAST_WALL) {
                    tiles[i,j] = Instantiate(_wallTiles[0]);
                    tiles[i,j].transform.position = new Vector3(i*widths[0], 0, j*1.15f - widths[0]/2);
                    tiles[i,j].transform.Rotate(0,90,0);
                    tiles[i,j].transform.SetParent(parentFolder);
                    tiles[i, j].name = "NE_wall_" + i + "_" + j;
                }
                else if (i == NORTH_WALL && j == WEST_WALL) {
                    tiles[i,j] = Instantiate(_wallTiles[0]);
                    tiles[i,j].transform.position = new Vector3(i*widths[0], 0, j*widths[0] + widths[0]/2);
                    tiles[i,j].transform.Rotate(0,90,0);
                    tiles[i, j].name = "NW_wall_" + i + "_" + j;
                    tiles[i,j].transform.SetParent(parentFolder);
                }
                else if (i == SOUTH_WALL && j == EAST_WALL) {
                    tiles[i,j] = Instantiate(_wallTiles[0]);
                    tiles[i,j].transform.position = new Vector3(i*widths[0], 0, j*1.15f - widths[0]/2);
                    tiles[i,j].transform.Rotate(0,90,0);
                    tiles[i, j].name = "SE_wall_" + i + "_" + j;
                    tiles[i,j].transform.SetParent(parentFolder);
                }
                else if (i == SOUTH_WALL && j == WEST_WALL) {
                    tiles[i,j] = Instantiate(_wallTiles[0]);
                    tiles[i,j].transform.position = new Vector3(i*widths[0], 0, j*widths[0] + widths[0]/2);
                    tiles[i,j].transform.Rotate(0,90,0);
                    tiles[i, j].name = "SW_wall_" + i + "_" + j;
                    tiles[i,j].transform.SetParent(parentFolder);
                }
                
            }
        }
    }
}
