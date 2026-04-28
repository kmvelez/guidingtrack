using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class SceneSwitch : MonoBehaviour
{public int scene = 1;
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(scene);
    }
}

