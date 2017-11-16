using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish : MonoBehaviour
{

    protected float m_moveSpeed = 2.0f;
    protected float m_life = 15;
    public SpriteRenderer render;
    public bool isHit = false;
    public float hitTimer = 0.1f;


    public enum Target
    {
        Left = -1,
        Right = 1
    }

    public Target m_target = Target.Left;
    protected Vector3 m_targetPosition;

    void Start()
    {
        render = this.GetComponent<SpriteRenderer>();
        SetTarget();
    }

    void Update()
    {
        UpdatePosition();
        if (isHit)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0)
            {
                isHit = false;
                hitTimer = 0.1f;
                render.material.color = new Color(1, 1, 1, 1);
            }
        }
    }

    void SetTarget()
    {
        float rand = Random.value;
        Vector3 scale = this.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (m_target == Target.Right ? 1 : -1);
        this.transform.localScale = scale;

        float cameraz = Camera.main.transform.position.z;
        m_targetPosition = Camera.main.ViewportToWorldPoint(new Vector3((int)m_target, 1 * rand, -cameraz));
    }

    void UpdatePosition()
    {
        Vector3 pos = Vector3.MoveTowards(this.transform.position, m_targetPosition, m_moveSpeed * Time.deltaTime);
        if (Vector3.Distance(pos, m_targetPosition) < 0.1f)
        {
            m_target = m_target == Target.Left ? Target.Right : Target.Left;
            SetTarget();
        }
        this.transform.position = pos;
    }

    public delegate void VoidDelegate(fish Fish);
    public VoidDelegate OnDeath;

    public static fish Create(GameObject prefab, Target target, Vector3 pos)
    {
        GameObject go = (GameObject)Instantiate(prefab, pos, Quaternion.identity);
        fish Fish = go.AddComponent<fish>();
        Fish.Init(target);
        return Fish;
    }

    void Init(Target target)
    {
        m_target = target;
    }

    public void SetDamage(int damage)
    {
        m_life -= damage;
        if (m_life <= 0)
        {
            GameObject prefab = Resources.Load<GameObject>("explosion");
            GameObject explosion = (GameObject)Instantiate(prefab, this.transform.position, this.transform.rotation);
            Destroy(explosion, 1.0f);

            OnDeath(this);
            Destroy(this.gameObject);
        }
    }
    // Use this for initialization


    // Update is called once per frame
}
