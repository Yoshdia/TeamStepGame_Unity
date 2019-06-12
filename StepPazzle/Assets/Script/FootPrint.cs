using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrint : MonoBehaviour
{

    [SerializeField]
    private GameObject[] foot = null;

    private List<GameObject> footList;

    private float spriteSizeX = 0;
    private float spriteSizeZ = 0;

    private System.Random random;

    private void Start()
    {
        footList = new List<GameObject>();
        random = new System.Random(foot.Length - 1);
    }

    //MapPositioning.Positioning()から呼ばれspriteSizeを受け取り保存する。
    public void SetSpriteSize(float x, float z)
    {
        spriteSizeX = x;
        spriteSizeZ = z;
    }

    public void SetFoot(Vector3 pos, Vector3 direction)
    {
        //そのままfootPosに足跡を設置すると進んでいない場所にはみ出してしまうため0.5ずらすためのVector
        Vector3 disPlaceVec = direction;
        disPlaceVec /= 2.0f;
        pos += disPlaceVec;
        Vector3 footPos = new Vector3(pos.x * spriteSizeX, 0.2f, pos.z * spriteSizeZ);
        direction.y = 90;
        Quaternion qua = Quaternion.LookRotation(direction);

        int range = random.Next(foot.Length);
        GameObject newFoot = Instantiate(foot[range], footPos, qua);
        newFoot.transform.parent = transform;
        footList.Add(newFoot);
    }

    public void DeleteOneFoot()
    {
        footList[footList.Count - 1].SetActive(false);
        footList.RemoveAt(footList.Count - 1);
        //Destroy(footList[footList.Count - 1]);
    }
}
