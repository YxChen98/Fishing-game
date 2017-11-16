using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour {

    float m_shootTimer = 0;
    public AudioClip m_fireAudio;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_shootTimer -= Time.deltaTime;
        UpdateInput();
	}

    void UpdateInput()
    {
        Vector3 ms = Input.mousePosition;
        ms = Camera.main.ScreenToWorldPoint(ms);

        Vector3 mypos = this.transform.position;
        float angle;

        if (Input.GetMouseButton(0))
        {
            Vector2 targetDir = ms - mypos;
            angle = Vector2.Angle(targetDir, Vector3.up);

            if (ms.x > mypos.x)
                angle =- angle;

            this.transform.eulerAngles = new Vector3(0, 0, angle);

            if (m_shootTimer <= 0)
            {
                m_shootTimer = 0.1f;
                fire.Create(this.transform.TransformPoint(0, 1, 0), new Vector3(0, 0, angle));
                this.GetComponent<AudioSource>().PlayOneShot(m_fireAudio);
            }
        }
    }
}
