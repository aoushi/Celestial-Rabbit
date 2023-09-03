using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public void reloadGameScene()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(1);
    }
}
