using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    [SerializeField] private GameObject projectileStartPos;

    [SerializeField] private GameObject[] weapons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(weapons[0], projectileStartPos.transform.position, weapons[0].transform.rotation);
        }
    }
}
