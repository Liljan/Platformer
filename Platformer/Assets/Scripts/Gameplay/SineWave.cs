using UnityEngine;
using System.Collections;

public class SineWave : MonoBehaviour
{
    public Vector2 amp;
    private float phase = Mathf.PI/2;
    public float speed = 1.0f;

    private Vector3 startPos;

    public void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + new Vector3(amp.x * Mathf.Cos(phase), amp.y * Mathf.Sin(phase), 0.0f);
        phase += speed * Time.deltaTime;
    }
}
