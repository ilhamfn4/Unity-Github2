using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitObject : MonoBehaviour {

    public string TitleFF;
    public string DescFF;

    public GameObject prefabFF;

    public GameObject panelFF;
    private LineRenderer liner;
    private Transform mainCamera;

    private LineManager lineManager;
    private bool isActive = false;

    private void Awake() {

        mainCamera = Camera.main.transform;
        lineManager = FindObjectOfType<LineManager>();

    }

    private void Update () {

        if (Input.GetMouseButtonDown(0) && panelFF == null) {

            Ray ray;

            RaycastHit hit;

            #if UNITY_EDITOR
                 ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            #elif UNITY_ANDROID
		         ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            #endif

            if (Physics.Raycast(ray, out hit, 100.0f)) {

                if (hit.transform == transform) {

                    panelFF = Instantiate(prefabFF, Vector3.zero, Quaternion.identity, this.transform);

                    panelFF.transform.localPosition = new Vector3(0.0f, 4f, 0.0f);

                    Text title = panelFF.transform.Find("PanelFunFact").transform.Find("Title_txt").GetComponent<Text>();
                    Text desc = panelFF.transform.Find("PanelFunFact").transform.Find("Description_txt").GetComponent<Text>();

                    lineManager.objectHitted = hit;

                    title.text = TitleFF;
                    desc.text = DescFF; 
                }

            } 
        }

        if (transform.childCount  > 0) {
            createLine(transform.position, panelFF.transform.position);

            Debug.DrawLine(transform.position, panelFF.transform.position);

            //panelFF.transform.LookAt(mainCamera);
            //panelFF.transform.Rotate(0, 180, 0);
            panelFF.transform.rotation = Quaternion.Euler(mainCamera.rotation.x, panelFF.transform.rotation.y, mainCamera.rotation.z);
            //this.transform.rotation.SetLookRotation(mainCamera.position);

        }

    }

	private void createLine(Vector3 FirstPoint, Vector3 EndPoint) {

        if (liner == null)
            liner = gameObject.AddComponent<LineRenderer>();

        liner.SetPosition (0, FirstPoint);
		liner.SetPosition (1, EndPoint);
        
        liner.startWidth = 0.1f;
        liner.endWidth = 0.1f;
        //liner.loop = true;
        liner.receiveShadows = false;
        liner.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

	}

}
