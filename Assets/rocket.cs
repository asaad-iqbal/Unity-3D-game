using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class rocket : MonoBehaviour
{
    //rotation speed
    [SerializeField] float rcsThrust = 250f;
    //movement speed
    [SerializeField] float mainThrust = 2000f;
    //audio clip to run in game
    [SerializeField] AudioClip mainEngine;
    Rigidbody rigidBody;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false;
    }

    private void Thrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("ok");
                break;
            case "Obstacle":
                SceneManager.LoadScene(0);
                print("starting level 1");
                break;
            case "level1":
                print("Finished level 1");
                SceneManager.LoadScene(1);
                break;
            case "level2":
                print("Finished level 2");
                SceneManager.LoadScene(2);
                break;
            case "level3":
                print("Finished level 3");
                SceneManager.LoadScene(0);
                break;
            default:
                print("Hit ground");
                break;
        }
    }
}
