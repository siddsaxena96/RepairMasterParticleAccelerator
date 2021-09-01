using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoadScript : MonoBehaviour
{
    public string sceneName;
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
