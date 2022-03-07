using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerState : MonoBehaviour
{

    public int amountCorrectConnect;
    public UnityAction<int> OnCorrectConnect;

    // Start is called before the first frame update
    void Start()
    {
        amountCorrectConnect = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectConnect()
    {
        amountCorrectConnect++;
        OnCorrectConnect(amountCorrectConnect);
    }
}
