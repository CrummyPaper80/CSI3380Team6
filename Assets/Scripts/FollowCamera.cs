 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class FollowCamera: MonoBehaviour {
 //make the background follow the camera
     private float camera_x;
     private float camera_y;
 
     // Use this for initialization
     void Start () {
         
     }
     
     // Update is called once per frame
     void LateUpdate () {
         camera_x = Camera.main.transform.position.x;
         camera_y = Camera.main.transform.position.y;
         transform.position = new Vector3(camera_x, camera_y, transform.position.z);
     }
 }