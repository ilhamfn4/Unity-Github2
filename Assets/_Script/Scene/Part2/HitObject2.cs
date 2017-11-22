using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitObject2 : MonoBehaviour {

    public string TitleFF;
    public string DescFF;

    public bool isSelected;
    public RaycastHit hit;

    private LineManager2 lineManager;

    private void Awake() {

        lineManager = FindObjectOfType<LineManager2>();

    }

    private void Update() {
        
        if (Input.GetMouseButtonDown(0)) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, 100.0f)) {

                if (hit.transform.gameObject.name == this.gameObject.name) {

                    isSelected = true;

                    Debug.Log("Hit");

                    lineManager.objectHitted = hit;
                    lineManager.panelFF.SetActive(true);

                    Text title = lineManager.panelFF.transform.Find("PanelFunFact").transform.Find("Title_txt").GetComponent<Text>();
                    Text desc = lineManager.panelFF.transform.Find("PanelFunFact").transform.Find("Description_txt").GetComponent<Text>();

                    title.text = TitleFF;
                    desc.text = DescFF;
                    
                }

            }

        }
        

    }

}
