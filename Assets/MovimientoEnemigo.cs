using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public float speed = 4f;  // Velocidad de movimiento del enemigo

    private void Start()
    {
        StartCoroutine(MoveInPattern());
    }

    IEnumerator MoveInPattern()
    {
        while (true)
        {
            // Mover horizontalmente durante 3 segundos
            yield return Move(Vector3.right, 2f);

            // Mover verticalmente durante 3 segundos
            yield return Move(Vector3.left, 2f);

            yield return Move(Vector3.up, 2f);
        }
    }

    IEnumerator Move(Vector3 direction, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            transform.position += direction * speed * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;  // Espera hasta el siguiente frame
        }
    }
}

