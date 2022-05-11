using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOffice : MonoBehaviour
{

    public GameObject PlayerData;
    // Start is called before the first frame update
    void Awake()
    {
        var data = FindObjectOfType<PlayerData>();
        if (data == null)
        {
            Instantiate(PlayerData);
        }

        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
