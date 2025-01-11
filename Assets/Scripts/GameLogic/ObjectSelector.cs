using UnityEngine;


public class ObjectSelector : MonoBehaviour
{
    public bool isSelected;
    GameObject selectMarker;
    GameManager gameManager;

    void Start(){
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.onSelect.AddListener((go) => onSelectOther(go));
        selectMarker = gameObject.transform.Find("SelectMarker").gameObject;
    }
    void OnMouseDown(){
        isSelected = true;
        if(selectMarker) selectMarker.SetActive(true);
        
        Debug.Log("select " + gameObject.name);
        gameManager.selectNewObject(gameObject);
    }


    void onSelectOther(GameObject obj){
        if (obj != gameObject) {
            isSelected = false;
            if(selectMarker) selectMarker.SetActive(false);
        }
    }
}
