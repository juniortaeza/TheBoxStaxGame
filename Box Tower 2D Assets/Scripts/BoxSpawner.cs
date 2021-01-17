using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject box_Prefab;
    public BoxScript script;

    private int boxes = 0;
    //declare sound
    public void SpawnBox()
    {
        boxes++;
        //play sound
        GameObject box_Obj = Instantiate(box_Prefab);
        /* Rigidbody2D boxbody = box_Obj.GetComponent<Rigidbody2D>();
         boxbody.velocity = */

        script = box_Obj.GetComponent<BoxScript>();
        script.move_Speed = script.move_Speed + (float)(boxes*.1);

        //Setting z axis to 0
        Vector3 temp = transform.position;
        temp.z = 0f;
        box_Obj.transform.position = temp;

        
    }
    
    

}
