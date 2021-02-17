using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public Rigidbody body;
    public float speed;
    public float rotationSpeed;

    public RagDoll ragDoll;

    public Transform centerOfMass;

    public float flex = 0.5f;
    public float flexSpeed;
    public float startFlex;
    public float toFlex;

    public  bool tackling = false;

    private void Awake()
    {
        this.body.centerOfMass = this.centerOfMass.localPosition;
    }

    public IEnumerator CoTackle()
    {
        this.ragDoll.animator.SetBool("tackling", true);
        this.body.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.15f);
        this.body.velocity = 2f * this.speed * this.transform.forward;

        if (this.ragDoll.animator.enabled)
        {
            yield return new WaitForSeconds(0.6f);
            this.ragDoll.inertiate(this.body.velocity);
            this.StartCoroutine(CoRestore());
        }
    }

    public IEnumerator CoRestore()
    {
        yield return new WaitForSeconds(3f);
        this.tackling = false;
        this.ragDoll.animator.SetBool("tackling", false);
        this.ragDoll.animator.SetBool("running", false);
        this.ragDoll.restore();
    }

    public void crash()
    {
        if (this.ragDoll.animator.enabled)
        {
            this.ragDoll.inertiate(this.body.velocity + 4f * Vector3.up);
            this.StartCoroutine(CoRestore());
        }
    }
}
