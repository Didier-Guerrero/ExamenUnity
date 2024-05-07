using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController3D : MonoBehaviour
{
    public float moveSpeed = 1f;    // Velocidad de movimiento del jugador
    public float jumpForce = 3f;   // Fuerza de salto del jugador
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 originalGravity;
    private int ContadorSalto = 0;
    private int SaltosMax = 2;
    public float threshold;
    public Vector2 resetPosition = new Vector2(-49f, 3.45f); // Posición de reinicio configurada aquí para fácil acceso y modificación

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalGravity = Physics.gravity;
        Physics.gravity *= 3;
    }

    void Update()
    {
        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);


        // Saltar
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || ContadorSalto < SaltosMax))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            ContadorSalto++;
        }

        // Modificar la gravedad cuando el jugador no esté en el suelo
        if (!isGrounded)
        {
            Physics.gravity = originalGravity * 3f; // Duplicar la gravedad
        }
        else
        {
            Physics.gravity = originalGravity; // Restaurar la gravedad original
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el jugador está en el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            ContadorSalto = 0;
        }
        else if (collision.gameObject.CompareTag("Obstaculo"))
        {
            // Reiniciar la posición del jugador cuando choca con un obstáculo
            transform.position = resetPosition;
            ContadorSalto = 0;  // Opcional: reiniciar los saltos también si es necesario
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Victoria"))
        {
            // Cambiar el color del jugador
            GetComponent<Renderer>().material.color = Color.green;

            // Hacer que el jugador salte automáticamente
            StartCoroutine(AutoJump());
        }
    }

    IEnumerator AutoJump()
    {
        while (true)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                yield return new WaitForSeconds(1);  // Ajusta este tiempo según desees para la frecuencia de salto
            }
            yield return null;
        }
    }

    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = resetPosition;
        }
    }
}
