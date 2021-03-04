using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemies : MonoBehaviour
{
    public float speed;
    private float masse;
    private float gravity;

    private GameObject character;
    private CharacterController controller;
    private Vector3 mouvement;
    private BoxCollider detectionZone;

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
        mouvement.x = speed;
        controller.Move(mouvement * Time.deltaTime);
    }
    
    void Rotate()
    {
        character.transform.Rotate(0,180,0);
    }
}
