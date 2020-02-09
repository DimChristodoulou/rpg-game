using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class levelGeneration : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> _floorTiles;
    [SerializeField] private List<GameObject> _wallTiles;
    [SerializeField] private List<GameObject> _environmentalProps;
    [SerializeField] private TMP_InputField xDim;
    [SerializeField] private TMP_InputField yDim;
    [SerializeField] private Transform parentFolder;

    private int EAST_WALL = 0;
    private int WEST_WALL;
    private int NORTH_WALL = 0;
    private int SOUTH_WALL;
    private int x, y;
    
    private GameObject[,] tiles;
    private List<float> floorWidths, floorLengths, wallWidths, wallLengths;
    

    public void prepareRoomGeneration() {
        //Get editor values for dimensions of room
        x = int.Parse(xDim.text);
        y = int.Parse(yDim.text);

        EAST_WALL = 0;
        WEST_WALL = y-1;
        NORTH_WALL = 0;
        SOUTH_WALL = x-1;

        //Allocate space for the tiles array
        tiles = new GameObject[x, y];
        
        //Allocate space for the floorWidths and floorLengths lists
        floorWidths = new List<float>();
        floorLengths = new List<float>();
        wallWidths = new List<float>();
        wallLengths = new List<float>();
        
        generateRoom();
    }
    
    
    
    public void generateRoom()
    {
        //Basic algorithm
        //    Step 1: Create floor tiles for a rectangular room with given x,y dimensions
        //    Step 2: Create walls in edges and corners
        //    Step 3: Cut areas from edge tiles given a width and height (e.g. cut a 3x3 area in a 15x15 room)
        //             in order to create random shaped rooms
        //    Step 4: Beautify with random columns and environmental elements
        
        destroyRoom();

        updateFloorWidthsAndLengths();
        updateWallWidthsAndLengths();
        debugWidthsAndLengths();
        
        generateFloor();
        //generateWalls();
        //generateEnvironmentalProps();
    }

    private void debugWidthsAndLengths()
    {
        for (int i = 0; i < floorWidths.Count; i++) {
            Debug.Log("Floor " + i + " width: " + floorWidths[i] + " length: " + floorLengths[i]);
        }
        
        for (int i = 0; i < wallWidths.Count; i++) {
            Debug.Log("Wall " + i + " width: " + wallWidths[i] + " length: " + wallLengths[i]);
        }
    }

    private void updateFloorWidthsAndLengths() {
        foreach (GameObject tile in _floorTiles){
            floorWidths.Add(tile.GetComponent<MeshRenderer>().bounds.size.z);
            floorLengths.Add(tile.GetComponent<MeshRenderer>().bounds.size.x);
        }
    }

    private void updateWallWidthsAndLengths() {
        foreach (GameObject tile in _wallTiles){
            wallWidths.Add(tile.GetComponent<MeshRenderer>().bounds.size.z);
            wallLengths.Add(tile.GetComponent<MeshRenderer>().bounds.size.x);
        }
    }
    
    private void destroyRoom() {
        //Destroy the room
        foreach (Transform child in parentFolder) {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void generateFloor()
    {
        for (int i = 0; i < x; i++){
            for (int j = 0; j < y; j++) {
                //Create an x BY y array of tiles and place them in the correct distance
                tiles[i,j] = Instantiate(_floorTiles[Random.Range(0, _floorTiles.Count)]);
                tiles[i,j].transform.position = new Vector3(i*floorWidths[0], 0, j*floorWidths[0]);
                tiles[i,j].transform.SetParent(parentFolder);
                tiles[i, j].name = "floor_" + i + "_" + j;
            }
        }
    }

    private void generateEnvironmentalProps() {
        int numOfProps = Random.Range(0, x * y);
        Debug.Log(numOfProps);
        List<Tuple<int, int>> occupiedSpaces = new List<Tuple<int, int>>();
        Tuple<int, int> coords = new Tuple<int, int>(Random.Range(0, x), Random.Range(0, y));
        GameObject prop = null;
        
        for (int i = 0; i < numOfProps; i++) {
            while (occupiedSpaces.Contains(coords)) {
                coords = new Tuple<int, int>(Random.Range(0, x), Random.Range(0, y));
            }
            
            occupiedSpaces.Add(coords);
            prop = Instantiate(_environmentalProps[Random.Range(0, _environmentalProps.Count)]);
            prop.transform.position = new Vector3(coords.Item1*floorWidths[0], 0, coords.Item2*floorWidths[0]);
            prop.transform.SetParent(parentFolder);
            prop.name = "prop_" + coords.Item1 + "_" + coords.Item2;
        }
    }

    private void generateWalls() {
        for (int i = 0; i < x; i++) {
            for (int j = 0; j < y; j++) {
                //Create horizontal & vertical walls
                if (i == NORTH_WALL) {
                    createWall(i, j, "north", i-floorWidths[0]/2, j*floorWidths[0]);
                }
                else if (i == SOUTH_WALL) {
                    createWall(i, j, "south", i*floorWidths[0] + floorWidths[0]/2, j*floorWidths[0]);
                }
                else if (j == EAST_WALL) {
                    createWall(i, j, "east", i*floorWidths[0], j*1.15f - floorWidths[0]/2);
                }
                else if (j == WEST_WALL) {
                    createWall(i, j, "west", i*floorWidths[0], j*floorWidths[0] + floorWidths[0]/2);
                }
                
                //Create corners
                if (i == NORTH_WALL && j == EAST_WALL) {
                    createCornerWall(i, j, "NE", i*floorWidths[0], j*1.15f - floorWidths[0]/2);
                }
                else if (i == NORTH_WALL && j == WEST_WALL) {
                    createCornerWall(i, j, "NW", i*floorWidths[0], j*floorWidths[0] + floorWidths[0]/2);
                }
                else if (i == SOUTH_WALL && j == EAST_WALL) {
                    createCornerWall(i, j, "SE", i*floorWidths[0], j*1.15f - floorWidths[0]/2);
                }
                else if (i == SOUTH_WALL && j == WEST_WALL) {
                    createCornerWall(i, j, "SW", i*floorWidths[0], j*floorWidths[0] + floorWidths[0]/2);
                }
            }
        }
    }

    private void createWall(int i, int j, string wall_type, float xPos, float zPos) {
        tiles[i,j] = Instantiate(_wallTiles[Random.Range(0, _wallTiles.Count)]);
        tiles[i,j].transform.position = new Vector3(xPos, 0, zPos);
        
        if ((j == EAST_WALL || j == WEST_WALL) && i!=NORTH_WALL && i!=SOUTH_WALL) {
            tiles[i,j].transform.Rotate(0,90,0);
        }
        
        tiles[i,j].transform.SetParent(parentFolder);
        tiles[i,j].name = wall_type + "_wall_" + i + "_" + j;
    }

    private void createCornerWall(int i, int j, string wall_type, float xPos, float zPos) {
        tiles[i,j] = Instantiate(_wallTiles[Random.Range(0, _wallTiles.Count)]);
        tiles[i,j].transform.position = new Vector3(xPos, 0, zPos);
        tiles[i,j].transform.Rotate(0,90,0);
        tiles[i, j].name = wall_type + "_wall_" + i + "_" + j;
        tiles[i,j].transform.SetParent(parentFolder);
    }
}
