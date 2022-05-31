using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectsAddScore : MonoBehaviour
{
    private Vector3 vel1 = Vector3.zero;
    private float speed = 1f;
    private Vector3 vel3 = (Vector3)Vector2.zero;
    private Vector2 newPos;
    private Vector3 newScale;
    private float vel4;
    public Text text;

    private void Start()
    {
        this.newPos = (Vector2)(this.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1f, 2.5f)));
        this.newScale = this.transform.localScale * 0.7f;
    }

    private void Update()
    {
        this.transform.position = Vector3.SmoothDamp(this.transform.position, (Vector3)this.newPos, ref this.vel1, this.speed);
        this.transform.localScale = Vector3.SmoothDamp(this.transform.localScale, this.newScale, ref this.vel3, this.speed);
        Object.Destroy((Object)this.gameObject);
    }
}
