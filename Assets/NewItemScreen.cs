using DG.Tweening;
using TurtleThrower;

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

	private float timer = 0f;
	private float closeTimeout = 2f;

	public void ShowNewItem(CollectableItem item)
	{
		var memento = item.GetComponentInChildren<MementoScript>();

		if (memento == null)
		{
			return;
		}

		CanvasAlpha.DOFade(1, 0.3f).SetUpdate(true);
		
		transform.localScale = Vector3.zero;

		transform.DOScale(1, 0.3f).SetUpdate(true);

		ItemName.text = memento.ItemName;
		ItemLore.text = memento.ItemLore;

		ItemIcon.sprite = memento.ItemIcon;

		Time.timeScale = 0f;

		shown = true;

		timer = 0;
	}

	private void Hide()
	{
		transform.DOScale(0, 0.3f).SetUpdate(true);

		CanvasAlpha.DOFade(0, 0.5f).SetUpdate(true);

		Time.timeScale = 1f;
	}

	private void Update()
	{
		timer += Time.unscaledDeltaTime;
		
		if (timer > closeTimeout && shown && Input.anyKey)
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
