using UnityEngine;

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
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range) && hit.transform.name != "Player" ) //Not execute when accidently shooting yourself (looking down for example). Show no particles etc
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
                    Debug.Log("oh no, wrong");
                    FindObjectOfType<GameManager>().GameOver();
                }
                //if target has tag not_belong, player made correct choice
                if (target.tag == "not_belong")
                {
                    Debug.Log("yeh. you can now sit and solve the puzzle");
                }
            }
            //add force to the object that has been hit, but only if it has a rigidbody
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //Show the impactEffect. Instiate it at the point of the hit and we want to rotate it on the hit surface (bounce of idea). Destroy the gameobject afterwards
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
            //play shootaudio
            shootsound.Play();
        }
    }
}
