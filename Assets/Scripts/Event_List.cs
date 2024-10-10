using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_List : MonoBehaviour
{
    private CharacterController player;
    public GameObject sphere;
    public GameObject cube;
    void Start()
    {
        player = FindFirstObjectByType<CharacterController>();
    }

    public void Cube()
    {
        GameObject newCube = Instantiate(cube, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z+10), transform.rotation);
    }

    public void Sphere()
    {
        GameObject newSphere = Instantiate(sphere, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 10), transform.rotation);
    }
}
