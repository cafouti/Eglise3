using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemies : MonoBehaviour
{
    public float speed;
    public float masse;
    private float gravity;
    private bool poursuite = false;

    private GameObject character;
    private CharacterController controller;
    private Vector3 mouvement;
    private BoxCollider detectionZone;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        character = gameObject;
        controller = GetComponent<CharacterController>();
        detectionZone = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        if (controller.isGrounded && mouvement.y < 0)
        {
            mouvement.y = masse;
        }

        if (poursuite)
        {
            if (character.transform.position.x > player.transform.position.x)
            {
                mouvement.x = -speed;
            }
            else if (character.transform.position.x < player.transform.position.x)
            {
                mouvement.x = speed;
            }
        }
        else
        {
            mouvement.x = 0;
        }
        
        Debug.Log("Mouvement = " + mouvement);

        controller.Move(mouvement * Time.deltaTime);
    }
    
    void Rotate()
    {
        character.transform.Rotate(0,180,0);
    }

    void Poursuite(float x)
    {        
            if(character.transform.position.x > transform.position.x)
            {
                x = speed;
            }
            else if(character.transform.position.x > transform.position.x)
            {
                x = -speed;
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.name == "Fille(Clone)" || other.gameObject.name == "Monstre(Clone)")
        {
            Debug.Log(other.gameObject.name);
            player = other.gameObject.transform;
            poursuite = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Fille(Clone)" || other.gameObject.name == "Monstre(Clone)")
        {
            player = null;
            //poursuite = false;
        }
    }
}
