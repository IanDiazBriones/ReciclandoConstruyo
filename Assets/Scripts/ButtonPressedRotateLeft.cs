using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressedRotateLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	// Use this for initialization
	 void Start () {
	 
	 }
	 void Update()
	 {
	     
	 }
	 
	 public void OnPointerDown(PointerEventData eventData)
	 {
	    KeyControlScript.RotateLeft = true;
	 }
	 
	 public void OnPointerUp(PointerEventData eventData)
	 {
	     KeyControlScript.RotateLeft = false;
	 }
}
