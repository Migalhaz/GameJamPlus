using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class iLHACAIU : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string s = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(s);
        }
    }
}
