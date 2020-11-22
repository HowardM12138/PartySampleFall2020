using UnityEngine;

public class IWeapon : MonoBehaviour {

	public AudioClip fireSfx;

	public virtual void SwitchOn() {
		gameObject.SetActive(true);
	}

	public virtual void SwitchOff() {
		gameObject.SetActive(false);
	}

	public virtual void OnTriggerPressed() { }

	public virtual void OnTriggerReleased() { }

	public virtual bool CanFire() {
		return true;
	}
}