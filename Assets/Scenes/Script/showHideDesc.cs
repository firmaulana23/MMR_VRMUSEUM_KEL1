using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showHideDesc : MonoBehaviour
{
    public GameObject Desc;
    private bool Show = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showhideDesc(){
        if(!Show){
            Desc.SetActive(true);
            Show = true;
        }
        else{
            Desc.SetActive(false);
            Show = false;
        }
    }
}
