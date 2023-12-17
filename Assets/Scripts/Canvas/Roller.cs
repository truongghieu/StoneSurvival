using UnityEngine;
using UnityEngine.UI;

public class Roller : MonoBehaviour
{
    [SerializeField] private RawImage roller;

    [SerializeField] private float x,y;

    // Update is called once per frame
    void Update()
    {
        roller.uvRect = new Rect(roller.uvRect.position + new Vector2(x,y) * Time.deltaTime, roller.uvRect.size);
    }
}
