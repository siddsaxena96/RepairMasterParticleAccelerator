using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class LevelController : MonoBehaviour
{
    public Transform launchPoint;
    public CinemachineVirtualCamera cam;
    public PlayableDirector levelCutscene;
    public UnityEngine.Events.UnityEvent OnLevelStart;
    public UnityEngine.Events.UnityEvent OnLevelEnd;
    public bool isLastLevel = false;

    public void StartLevel(int priority)
    {
        if (cam != null)
            cam.Priority = priority + 1;
        OnLevelStart?.Invoke();
    }

    public int GetPriority()
    {
        return cam.Priority;
    }

    public void EndLevel()
    {
        if (isLastLevel)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("FinalScene");
        }
        else
        {
            levelCutscene?.Play();
            OnLevelEnd?.Invoke();
        }
    }

    public void SetCamera()
    {
    }
}
