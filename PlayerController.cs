using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundery
{
    public float minX, maxX, minZ, maxZ;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Boundery boundery;
    public float tilt;

    public GameObject shot;
    public Transform shotSpawn;

    public float fireRate;
    private float nextFire;

    void Update()
    {

        if(Input.GetButton("Fire1") && Time.time>nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            gameObject.GetComponent<AudioSource>().Play();
        }
        
        

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");    


        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(moveHorizontal * speed , 0.0f , moveVertical * speed);

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x,boundery.minX,boundery.maxX),
            0.0f,
            Mathf.Clamp(rb.position.z,boundery.minZ,boundery.maxZ)       
            );
        rb.rotation = Quaternion.Euler(0.0f,0.0f,-tilt * moveHorizontal);
    }

    
}
