using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period;
    float movementFector;

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period; //grows continuanlly from 0

        const float tau = Mathf.PI *2; //6.28
        float rawSinWave = Mathf.Sin(cycles * tau); //goes from -1 to +1

        movementFector = rawSinWave / 2f + 0.5f;

        Vector3 offset = movementFector * movementVector;
        transform.position = startingPos + offset;
    }
}
