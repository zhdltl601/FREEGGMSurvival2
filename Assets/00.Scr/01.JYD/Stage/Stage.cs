using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler
{
    [SerializeField] private string stateName;
    
            
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        print($"이 맵의 이름은{stateName}입니다.");
    }
}
