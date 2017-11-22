using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour {

    public GameObject[] hitObjects;
    public RaycastHit objectHitted;

    private void Update() {
        try {
            if (objectHitted.transform.name == hitObjects[0].name) {

                if (hitObjects[1].transform.childCount > 0 || hitObjects[2].transform.childCount > 0) {
                    Destroy(hitObjects[1].GetComponent<LineRenderer>());
                    Destroy(hitObjects[1].GetComponent<HitObject> ().panelFF);
                    Destroy(hitObjects[2].GetComponent<LineRenderer> ());
                    Destroy(hitObjects[2].GetComponent<HitObject>().panelFF);

                }

            }
            else if (objectHitted.transform.name == hitObjects[1].name) {

                if (hitObjects[0].transform.childCount > 0 || hitObjects[2].transform.childCount > 0) {
                    Destroy(hitObjects[0].GetComponent<LineRenderer>());
                    Destroy(hitObjects[0].GetComponent<HitObject>().panelFF);
                    Destroy(hitObjects[2].GetComponent<LineRenderer>());
                    Destroy(hitObjects[2].GetComponent<HitObject>().panelFF);
                }

            }
            else if (objectHitted.transform.name == hitObjects[2].name) {

                if (hitObjects[1].transform.childCount > 0 || hitObjects[0].transform.childCount > 0) {

                    Destroy(hitObjects[0].GetComponent<LineRenderer> ());
                    Destroy(hitObjects[0].GetComponent<HitObject>().panelFF);
                    Destroy(hitObjects[1].GetComponent<LineRenderer> ());
                    Destroy(hitObjects[1].GetComponent<HitObject>().panelFF);
                }

            }

        } catch { }
    }

}
