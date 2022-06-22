using UnityEngine;

namespace Project.Visuals.AssetStore.Combat_Magic_VFX_Vol._1.scripts
{
	public class fly : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
			transform.Translate(new Vector3(0.0f, 0.0f, -0.4f)); // задаем движение объекту вдоль
		}
	}
}
