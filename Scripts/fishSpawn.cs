using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishSpawn : MonoBehaviour {

    public float timer = 0;
    public int max_fish = 30;
    public int fish_count = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 1.0f;

            if (fish_count >= max_fish)
                return;

            int index = 1 + (int)(Random.value * 3.0f);
            if (index > 3)
                index = 3;

            fish_count++;
            GameObject fishprefab = (GameObject)Resources.Load("fish" + index);

            float cameraz = Camera.main.transform.position.z;
            Vector3 randpos = new Vector3(Random.value, Random.value, -cameraz);
            randpos = Camera.main.ViewportToWorldPoint(randpos);

            fish.Target target = Random.value > 0.5f ? fish.Target.Left : fish.Target.Right;
            fish f = fish.Create(fishprefab, target, randpos);
            f.OnDeath += OnDeath;           
        }
	}

    public void OnDeath(fish Fish)
    {
        fish_count--;
    }
}
