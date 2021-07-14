using LibUnity.Frontend;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(InfiniteScroll))]
public class ItemControllerLimited : UIBehaviour, IInfiniteScrollSetup {

	public long Max { get; set; }

	public void OnPostSetupItems()
	{
		var infiniteScroll = GetComponent<InfiniteScroll>();
		infiniteScroll.onUpdateItem.AddListener(OnUpdateItem);
		GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Elastic;

		var rectTransform = GetComponent<RectTransform>();
		var delta = rectTransform.sizeDelta;
		delta.y = infiniteScroll.itemScale * Max;
		rectTransform.sizeDelta = delta;
	}

	public void OnUpdateItem(int itemCount, GameObject obj)
	{
		if(itemCount < 0 || itemCount >= Max) {
			obj.SetActive (false);
		}
		else {
			obj.SetActive (true);
			
			var item = obj.GetComponentInChildren<EventItem>();
			item.UpdateItem(itemCount);
		}
	}
}
