using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager2 : MonoBehaviour {

    public GameObject panelFF;
    public HitObject2[] hitObjects;

    public RaycastHit objectHitted;

    private LineRenderer liner;

    private void Update() {
        
        if (liner == null)
            liner = panelFF.GetComponent<LineRenderer>();

        try {
            if (objectHitted.transform.name == hitObjects[0].name) {

                hitObjects[1].isSelected = false;
                hitObjects[2].isSelected = false;

                createLine(panelFF.transform.position, hitObjects[0].transform.position);

            }
            else if (objectHitted.transform.name == hitObjects[1].name) {

                hitObjects[0].isSelected = false;
                hitObjects[2].isSelected = false;

                createLine(panelFF.transform.position, hitObjects[1].transform.position);

            }
            else if (objectHitted.transform.name == hitObjects[2].name) {

                hitObjects[0].isSelected = false;
                hitObjects[1].isSelected = false;

                createLine(panelFF.transform.position, hitObjects[2].transform.position);

            }
        }
        catch (System.Exception e) {
            Debug.Log(e.Message);
        }


    }

    public void createLine (Vector3 firstPosition, Vector3 secondPosition) {

        liner.SetPosition(0, firstPosition);
        liner.SetPosition(1, secondPosition);

    }

}
