using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public string nextScene = "Type Scene Name Here";
    

    public void ChangeScene()
    {

        SceneManager.LoadScene(nextScene);
    }

    
}
