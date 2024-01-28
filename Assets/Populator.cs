
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Populator : ScriptableWizard
{

    public GameObject replaceObject;
    public GameObject targetParent;

    [MenuItem("Tools/Replace Children With Prefab")]
    static void CreateWizard()
    {
        DisplayWizard<Populator>("Yeeter McSkeeter?", "Populate");
    }

    private void OnWizardCreate()
    {
        List<GameObject> newObjs = new List<GameObject>();

        for (int i = 0; i < targetParent.transform.childCount; i++) {
            var childGO = targetParent.transform.GetChild(i);
            var pos = childGO.transform.position;

            var newObj = Instantiate(replaceObject);
            newObj.transform.position = pos;

            newObjs.Add(newObj);

            Destroy(childGO.gameObject);
        }

        foreach(var newObj in newObjs) {
            newObj.transform.parent = targetParent.transform;
        }
    }
}