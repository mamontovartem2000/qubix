using UnityEngine;

namespace Project.Visuals.AssetStore.Combat_Magic_VFX_Vol._1.scripts
{
    public class hitfx : MonoBehaviour

    {

        public GameObject explosion; // drag your explosion prefab here

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter()
        {
            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(expl, 3); // delete the explosion after 3 seconds
        }

    }
}