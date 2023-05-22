using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPc : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.5f;

    private Vector3 m_moveDir;
    private Vector3 m_dashDirection;
    private float m_timerDash;

    private CharacterController m_characterController;
    private Animator m_animator;

    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        // �̵� �Է� �ޱ�
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        m_moveDir = new Vector3(x, 0f, z).normalized;

        // �뽬 �Է� �ޱ�
        if (Input.GetKeyDown(KeyCode.LeftShift) && m_timerDash > dashDuration)
        {
            m_dashDirection = m_moveDir;
            m_timerDash = 0f;
        }

        // �뽬 �̵� ó��
        if (m_timerDash <= dashDuration)
        {
            m_characterController.Move(m_dashDirection * dashSpeed * Time.deltaTime);
            m_timerDash += Time.deltaTime;
        }

    }
}
