using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour {

    float m_movSpeed = 10.0f;
    //public AudioClip m_hitAudio;

    public static fire Create(Vector4 pos, Vector3 angle)
    {
        GameObject prefab = Resources.Load<GameObject>("fire");
        GameObject fireSprite = (GameObject)Instantiate(prefab, pos, Quaternion.Euler(angle));
        fire f = fireSprite.AddComponent<fire>();
        Destroy(fireSprite, 2.0f);
        return f;
    }

    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D other)
    {
        fish f = other.GetComponent<fish>();
        if (f == null)
            return;
        else
        {
            f.SetDamage(1);
            f.render.material.color = new Color(1, 0, 0, 1);
            f.isHit = true;
            //this.GetComponent<AudioSource>().PlayOneShot(m_hitAudio);
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
        this.transform.Translate(new Vector3(0, m_movSpeed * Time.deltaTime, 0));
	}
}
