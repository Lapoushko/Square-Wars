using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    private Vector2 startPos;
    public float height;
    public float speed;

    private void Start()
    {
        this.startPos = (Vector2)this.transform.position;
    }

    private void Update()
    {
        this.transform.position = (Vector3)new Vector2(this.startPos.x, this.startPos.y + Mathf.PingPong(Time.time * this.speed, this.height));
    }
}
