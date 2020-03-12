using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsWalk : MonoBehaviour
{
    Vector3 playerAxis;
    Vector3 playerRotAxis;
    Vector3 headRotAxis;
    public CharacterController charac;
    public GameObject prefabProjectile;
    public GameObject head;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        playerAxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 globalmove = transform.TransformDirection(playerAxis);//local pra global 
        charac.SimpleMove(globalmove * 5);
        playerRotAxis = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        headRotAxis = new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);

        transform.Rotate(playerRotAxis);//gira o corpo
        head.transform.Rotate(headRotAxis);//gira cabeca

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject ball = Instantiate(prefabProjectile, transform.position+head.transform.forward, transform.rotation);
            ball.GetComponent<Rigidbody>().AddForce(head.transform.forward * 1000+ Vector3.up*200);
            ball.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.right*500, ForceMode.Impulse);
            Destroy(ball, 3);
        }
    }
}
