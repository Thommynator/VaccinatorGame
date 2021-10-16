using UnityEngine;

public class StaticRotation : MonoBehaviour
{
    void Update()
    {
        // ignore (parent) rotation, i.e. make sure that north is always up
        this.transform.rotation = Quaternion.identity;
    }
}
