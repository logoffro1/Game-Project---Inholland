
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleScene : MonoBehaviour {

    

    void Update() {
        
        if (Input.GetKeyDown(KeyCode.E)) {
            
            if (SceneManager.GetActiveScene().name.Equals("SampleSceneDay")) {

                SceneManager.LoadScene("SampleSceneNight");
            }
            else { 
                SceneManager.LoadScene("SampleSceneDay");
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            //LoadRewire
            Debug.Log("Enter was pressed");
            SceneManager.LoadScene("Rewire");
            Debug.Log("Rewire should be appearing somewhere...");
        }
    }
}
