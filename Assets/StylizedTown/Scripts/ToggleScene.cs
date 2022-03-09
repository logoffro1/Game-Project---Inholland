
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleScene : MonoBehaviour {

    

    void Update() {
        
        if (Input.GetKeyDown(KeyCode.E)) {
            
            //if (SceneManager.GetActiveScene().name.Equals("SampleSceneDay")) {

            //    SceneManager.LoadScene("SampleSceneDay");
            //}
            //else { 
            //    SceneManager.LoadScene("SampleSceneDay");
            //}
            
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            //LoadRewire
            Debug.Log("Enter was pressed");
            SceneManager.LoadScene("Rewire");
            Debug.Log("Rewire should be appearing somewhere...");
        }
    }
}
