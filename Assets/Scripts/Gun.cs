﻿using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 60f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource shootsound;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //Fire1 is a default button setup by unity
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play(); //Show particles
        RaycastHit hit;
        // we want to shoot forward starting from the camera and we want to info in the hit variable.The range is optional
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range) && hit.transform.name != "Player") //Not execute when accidently shooting yourself (looking down for example). Show no particles etc
        {
            //execute TakeDamage function from Target script
            Target target = hit.transform.GetComponent<Target>();
            //we only want to execute it if we actually found the component
            if (target != null)
            {
                target.TakeDamage(damage);
                //if target has tag belong, execute gameover from game master script
                if (target.tag == "belong")
                {
                    FindObjectOfType<GameManager>().GameOver();
                }
                //if target has tag not_belong, player made correct choice and can now see the sequence of the colored lanterns
                if (target.tag == "not_belong")
                {
                    FindObjectOfType<GameManager>().startPuzzle = true;
                }
            }
            //add force to the object that has been hit, but only if it has a rigidbody
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range) && hit.transform.tag == "wall_puzzle") //only execute when hitting wall puzzle
        {
            //Get the current hit block gameobject and find the parent to have access to the CheckPuzzle script attached to parent
            CheckPuzzle colorBlock = GameObject.FindGameObjectWithTag("wall_puzzle").GetComponentInParent<CheckPuzzle>();
            //Pass the name of the colorBlock as a string to the SaveColor method in CheckPuzzle Script
            colorBlock.SaveColor(hit.transform.name.ToString());
        }

        //Show the impactEffect. Instiate it at the point of the hit and we want to rotate it on the hit surface (bounce of idea). Destroy the gameobject afterwards
        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGO, 2f);
        //play shootaudio
        shootsound.Play();
    }
}