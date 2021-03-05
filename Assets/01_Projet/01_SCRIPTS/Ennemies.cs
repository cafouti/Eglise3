using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemies : MonoBehaviour
{
    public float speed;
    public float masse;
    private float gravity;
    private bool poursuite = false;
    private bool retour = false;
    private float offsetx = 0.2f;
    public bool droite;
    private float lastMovex;

    private GameObject character;
    private CharacterController controller;
    private Vector3 mouvement;
    private BoxCollider detectionZone;
    private Transform player;

    private Vector3 start;
    public Transform destination;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;        
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
               
        lastMovex = mouvement.x;

        if (poursuite)
        {
            Poursuite();
            Debug.Log("Poursuite = " + poursuite);
        }
        else if(retour)
        {
            Retour();
        }
        else
        {
            mouvement.x = 0;
        }

        if(mouvement.x == -1*lastMovex)
        {
            Debug.Log("Besoin de rotate");
        }
        
        controller.Move(mouvement * Time.deltaTime);
    }
    
    /////////////////////////Methode classe//////////////////////////

    void Rotate()
    {
        character.transform.Rotate(0,180,0);
    }

    void Poursuite()
    {
        if(player != null)
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
    }

    void Retour()
    {
        if (start.x > transform.position.x)
        {
            mouvement.x = speed;
        }
        else if (start.x < transform.position.x)
        {
            mouvement.x = -speed;
        }

        if(-offsetx < start.x - transform.position.x && start.x - transform.position.x < offsetx)
        {
            retour = false;
        }
    }

    /////////////////////////Detection/////////////////////

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.name == "Fille(Clone)" || other.gameObject.name == "Monstre(Clone)")
        {
            player = other.gameObject.transform;
            poursuite = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Fille(Clone)" || other.gameObject.name == "Monstre(Clone)")
        {
            StartCoroutine(PerteVue());
        }
    }

    //////////////////////Coroutine///////////////////////

    IEnumerator PerteVue()
    {
        //Debug.Log("Pertevue");
        yield return new WaitForSeconds(1);
        poursuite = false;
        player = null;
        StartCoroutine(PerteAgro());
    }

    IEnumerator PerteAgro()
    {
        yield return new WaitForSeconds(1);
        retour = true;
    }
}
