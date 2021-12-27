using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject win;
    [SerializeField] AudioSource audioSource;
    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            audioSource.Play();
            Instantiate(win);
        }
    }
}
