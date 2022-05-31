using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private Vector2 vel = Vector2.zero;
    private float speed = 0.05f;
    public int Score;
    public bool growing;
    public int HighScore;
    private Vector2 maxSize;
    public Text TextScore;

    private void Start()
    {
        this.Score = 0;
        maxSize = new Vector2(20f, 20f);
        HighScore = PlayerPrefs.GetInt("Highscore");
    }
    private void Update()
    {
        TextScore.text = Score.ToString();
        Vector2 vector2;
        if (this.growing)
        {
            vector2 = Vector2.SmoothDamp((Vector2)this.TextScore.transform.localScale, this.maxSize, ref this.vel, this.speed);
            if ((double)vector2.x > (double)this.maxSize.x - 8.100000001490116)
                this.growing = false;
        }
        else vector2 = Vector2.SmoothDamp((Vector2)this.TextScore.transform.localScale, new Vector2(8f, 8f), ref this.vel, this.speed);
        this.TextScore.transform.localScale =(Vector3)vector2;
    }

    public void AddScore(int points)
    {
        Score += points;
        growing = true;
    }

}
