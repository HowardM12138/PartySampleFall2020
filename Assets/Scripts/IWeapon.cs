using UnityEngine;

public class IWeapon : MonoBehaviour {

	public AudioClip fireSfx;

	public virtual void OnTriggerPressed() { }

	public virtual void OnTriggerReleased() { }

	public virtual bool CanFire() {
		return true;
	}
}