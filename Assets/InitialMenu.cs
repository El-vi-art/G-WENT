using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialMenu : MonoBehaviour
{
	//public GameObject menu;
    // Start is called before the first frame update
    public void PlayGame()
    {
    	Debug.Log("Botón clickeado");
        SceneManager.LoadScene(1);
    }
}
