using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ChangeTextColorOnButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

	public Color Highlighted;
	private Color Normal;

	void Start()
	{
		Normal = GetComponentInChildren<Text>().color;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		GetComponentInChildren<Text>().color = Highlighted;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		GetComponentInChildren<Text>().color = Normal;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		GetComponentInChildren<Text>().color = Normal;
	}
}