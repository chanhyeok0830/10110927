using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void restartScene()
    {
        SceneManager.LoadScene(0);
    }
}