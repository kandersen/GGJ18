using UnityEngine;
using DG.Tweening;
using System.Collections;

public class BaseItem : MonoBehaviour
{
    public ItemBehaviour Item;
    
	public SpriteRenderer FlashRenderer;

    public Transform JoinPoint1;
    public Transform JoinPoint2;

	private Coroutine _flashing;

    public enum CombinationResult
    {
        NotDone = 0,
        Success = 1,
        Switchable = 2,
        Dud = 3,
    }

    public ItemBehaviour first;
    public ItemBehaviour second;

	public void Start() {
		if (FlashRenderer != null) {
			FlashRenderer.enabled = false;
		}
	}

	public void StartFlashing() {
		_flashing = StartCoroutine (StartFlashingAux ());
	}

	private IEnumerator StartFlashingAux() {
		Debug.Log ("Flashing start");
		if (_flashing != null) {
			StopCoroutine (_flashing);
		}
		float init = FlashRenderer.color.a;
		FlashRenderer.color = new Color (1, 1, 1, 0);
		FlashRenderer.enabled = true;
		if (first != null && first.AddOn.FlashRenderer != null) {
			first.AddOn.FlashRenderer.color = new Color (1, 1, 1, 0);
			first.AddOn.FlashRenderer.enabled = true;
			first.AddOn.FlashRenderer.DOFade (init, 0.1f);
		}
		if (second != null && second.AddOn.FlashRenderer != null) {
			second.AddOn.FlashRenderer.color = new Color (1, 1, 1, 0);
			second.AddOn.FlashRenderer.enabled = true;
			second.AddOn.FlashRenderer.DOFade (init, 0.1f);
		}
		yield return FlashRenderer.DOFade(init,0.1f).WaitForCompletion();
	}

	public void StopFlashing() {
		_flashing = StartCoroutine (StopFlashingAux());
	}

	public IEnumerator StopFlashingAux() {
		Debug.Log ("Flashing stop");
		if (_flashing != null) {
			StopCoroutine (_flashing);
		}
		if (first != null) {
			first.AddOn.FlashRenderer.DOFade (0, 0.1f);
		}
		if (second != null) {
			second.AddOn.FlashRenderer.DOFade (0, 0.1f);
		}
		yield return FlashRenderer.DOFade (0, 0.1f).WaitForCompletion();
		FlashRenderer.enabled = false;
	}

    public void Attach(ItemBehaviour item)
    {
		if (second == null && item.AddOn.Class == AddOn.ItemClass.Antenna) {
			item.transform.parent = JoinPoint2;
			item.transform.localPosition = Vector3.zero;
			item.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 30));
			second = item;
			item.AddOn.Base = this;
		}
		else if (first == null && item.AddOn.Class == AddOn.ItemClass.Switch) {
			item.transform.parent = JoinPoint1;
			item.transform.localPosition = Vector3.zero;
			item.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 10));
			first = item;
			item.AddOn.Base = this;
		} 
		else if (first == null)
        {
            item.transform.parent = JoinPoint1;
            item.transform.localPosition = Vector3.zero;
            first = item;
			item.AddOn.Base = this;
        }
        else
        {
            item.transform.parent = JoinPoint2;
            item.transform.localPosition = Vector3.zero;
            second = item;
			item.AddOn.Base = this;
        }
      
    }

    public CombinationResult CheckCompletion()
    {
        if (first == null || second == null )
        {
            return CombinationResult.NotDone;
        }               

		if (first.AddOn == null || second.AddOn == null) {
			return CombinationResult.NotDone;
		}
        
        var switchable = first.AddOn.Class == AddOn.ItemClass.Switch ||
                         second.AddOn.Class == AddOn.ItemClass.Switch;

        if (!switchable) 
            return CombinationResult.Dud;

        var hasAntenna = first.AddOn.Class == AddOn.ItemClass.Antenna ||
                         second.AddOn.Class == AddOn.ItemClass.Antenna; 
        
        return hasAntenna ? CombinationResult.Success : CombinationResult.Switchable;
    }
}