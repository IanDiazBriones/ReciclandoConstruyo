using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressedRotateRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	// Use this for initialization
	 void Start () {
	 
	 }
	 void Update()
	 {
	     
	 }
	 
	 public void OnPointerDown(PointerEventData eventData)
	 {
	    KeyControlScript.RotateRight = true;
	 }
	 
	 public void OnPointerUp(PointerEventData eventData)
	 {
	     KeyControlScript.RotateRight = false;
	 }
}
