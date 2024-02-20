using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class CheckPermission : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LocationCheckPermission()
    {

    }

    void CamaraCheckPermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)&& 
            !Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) &&
            !Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
            Permission.RequestUserPermission(Permission.Camera);
        }
    }
}
