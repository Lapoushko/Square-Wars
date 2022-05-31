using UnityEngine;
using UnityEngine.UI;

public class UIChangedColor : MonoBehaviour
{
    [SerializeField]
    Gradient _gradient;
    Material _myMat;

    [SerializeField] float speedChanged = 0.3f;

    void Start()
    {
        this._myMat = GameObject.Find("GameName").GetComponent<Text>().material;
    }

    void Update()
    {
        this._myMat.color = this._gradient.Evaluate(Mathf.PingPong(Time.time * speedChanged, 1f));
    }
}