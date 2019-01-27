using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TurtleThrower;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NewItemScreen : MonoBehaviour
{
	public CanvasGroup CanvasAlpha;
	public Text ItemName;
	public Text ItemLore;
	public Image ItemIcon;
	
	public static NewItemScreen Instance;

	private bool shown = false;

	public void ShowNewItem(CollectableItem item)
	{
		var memento = item.GetComponent<MementoScript>();

		if (memento == null)
		{
			return;
		}

		CanvasAlpha.alpha = 0f;
		
		transform.localScale = Vector3.zero;

		transform.DOScale(1, 0.3f);

		ItemName.text = memento.ItemName;
		ItemLore.text = memento.ItemLore;

		ItemIcon.sprite = memento.ItemIcon;

		shown = true;
	}

	private void Hide()
	{
		transform.DOScale(0, 0.3f);

		CanvasAlpha.DOFade(0, 0.5f);
	}
	
	private void FixedUpdate()
	{
		if (shown && Input.anyKey)
		{
			Hide();
			shown = false;
		}
	}

	private void Awake()
	{
		Instance = this;
	}
}
