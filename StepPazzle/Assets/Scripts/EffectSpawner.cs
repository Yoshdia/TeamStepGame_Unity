using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    private float effectSize;
    private Vector3 spriteSize;

    [SerializeField]
    private GameObject effect = null;

    //ActiveでないObjectが格納されている配列
    private GameObject[] waitingEffect;
    //ActiveのObject。配列から足跡を受け取った時にアクティブにさせここに格納する
    private List<GameObject> activeEffect;

    ObjectPool pool;

    public void FirstProcces()
    {
        pool = GetComponent<ObjectPool>();


        waitingEffect = new GameObject[200];
        pool.CreatePool(effect, waitingEffect);
        
    }

    public void SetSpriteSize(Vector3 size)
    {
        spriteSize = size;
        spriteSize.z = 1.0f;
        effectSize = ((size.x > size.y) ? size.x : size.y) / 1;
    }

    public void SetEffect(Vector3 pos)
    {
        GameObject setPool=pool.GetWaitingObject(waitingEffect,pos,new Quaternion());
        //setPool.transform.localScale = new Vector3(effectSize, effectSize, effectSize);
        setPool.GetComponent<ParticleSystem>().startSize = effectSize*3.5f;
        setPool.transform.position = new Vector3(pos.x * effectSize, pos.y * effectSize, -0.1f);


    }

    public void DeleteEffect()
    {
        pool.ResetWaitingObject(waitingEffect);
    }

}
