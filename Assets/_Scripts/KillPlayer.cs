using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour {

    public float reloadDelay = 0.2f;
    Collider2D collidedObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        collidedObject = other;
        Invoke("Reload", reloadDelay);
    }

    void Reload()
    {
        if (collidedObject.gameObject.CompareTag("Player")){
            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
        }
    }
}
