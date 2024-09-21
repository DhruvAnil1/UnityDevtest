using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // Public methods to be assigned via the Inspector
    public void Retry()
    {
        SceneManager.LoadScene("Level1");
        
    }

    
}
