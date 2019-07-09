using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsLife : MonoBehaviour
{
    [SerializeField]
    int CountMax=100;
    int count = 0;
    bool reset = false;

    // Start is called before the first frame update
    public void ResetEffect()
    {
        count = CountMax;
        reset = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!reset)
        {
            ResetEffect();
        }
        count--;
        if(count < 0)
        {
            gameObject.SetActive(false);
            reset = false;
        }
    }
}
