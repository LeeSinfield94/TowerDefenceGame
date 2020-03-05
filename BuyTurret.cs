using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BuyTurret : MonoBehaviour, IPointerDownHandler
{
    private Vector3 MousePos;
    private bool SpawnedTurret;
    public GameObject Turret;
    private void OnMouseDown()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!SpawnedTurret)
        {
            Instantiate(Turret);
            SpawnedTurret = true;
        }
        MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        Turret.transform.position = new Vector2(MousePos.x, MousePos.y);
    }
}
