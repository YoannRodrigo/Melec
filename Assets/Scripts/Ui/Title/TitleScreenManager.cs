using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreenManager : MonoBehaviour
{

    public string selectedMenu;

    public GameObject buttonsGO;
    public GameObject arrow;
    

    // Update is called once per frame
    void Update() {
        if (selectedMenu == "") {
            selectedMenu = EventSystem.current.currentSelectedGameObject.name;
        }else if(EventSystem.current.currentSelectedGameObject.name != selectedMenu) {
            selectedMenu = EventSystem.current.currentSelectedGameObject.name;
            OnChangeSelectedGO();
        }
    }
    
    public void OnChangeSelectedGO(){
        arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x,EventSystem.current.currentSelectedGameObject.transform.localPosition.y,arrow.transform.localPosition.z);
    }
}
