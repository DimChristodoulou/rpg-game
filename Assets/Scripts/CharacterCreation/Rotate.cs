using UnityEngine;
using UnityEngine.EventSystems;

public class Rotate : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Output the name of the GameObject that is being clicked
        Debug.Log(name + "Game Object Click in Progress");
        if(name == "rotateClockwiseBtn")
            rotate(true);
        else
            rotate(false);
    } 
    
    public void rotate(bool clockwise)
    {
        if (clockwise)
        {
            GameObject[] activeClassModel = GameObject.FindGameObjectsWithTag("Class3DGameObjects");
            activeClassModel[0].transform.Rotate(Vector3.up, 5);
        }
        else
        {
            GameObject[] activeClassModel = GameObject.FindGameObjectsWithTag("Class3DGameObjects");
            Debug.Log(activeClassModel[0].name);
            activeClassModel[0].transform.Rotate(Vector3.up, -5);
        }
    }
}
