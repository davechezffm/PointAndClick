using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTag : MonoBehaviour
{
    Vector2 resolution, resolutionInWorldUnits = new Vector2(17.8f,10);
    public bool overFlow;
    // Start is called before the first frame update
    void Start()
    {
        resolution = new Vector2(Screen.width, Screen.height);
    }

   
    void LateUpdate()
    {


        FollowMouse(); 
    }

    private void FollowMouse()
    
     {
        if (transform.position.x<=14)

        { transform.position = Input.mousePosition / resolution * resolutionInWorldUnits; }

        if (transform.position.x >= 14)
        {
            
            transform.position =new Vector2((Input.mousePosition.x / Screen.width * 17.8f)-5, Input.mousePosition.y / Screen.height * 10);          

        }
    }

    
}
