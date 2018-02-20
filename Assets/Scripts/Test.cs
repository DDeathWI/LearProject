using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour {

    void Start() { 

        PlayerPrefs.SetInt("health", 100);

        SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
    }
	
	
}
