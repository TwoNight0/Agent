using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour{

    private Vector3 m_rotationValue;


    // Start is called before the first frame update
    void Start(){
        m_rotationValue = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update(){
        Rotation();
        followcam();
    }
    
    private void Rotation(){
        
        float MouseX = Input.GetAxisRaw("Mouse X") * PlayerMng.Instance.mouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxisRaw("Mouse Y") * PlayerMng.Instance.mouseSensitivity * Time.deltaTime;

        m_rotationValue.x += MouseY * -1f;
        m_rotationValue.y += MouseX;

        //각도제한
        m_rotationValue.x = Mathf.Clamp(m_rotationValue.x, 0f, 50f);
        //m_rotationValue.y = Mathf.Clamp(m_rotationValue.y, -90f, 90f);


        transform.rotation = Quaternion.Euler(m_rotationValue.x, m_rotationValue.y, 0f);
        //m_camFps.transform.rotation = Quaternion.Euler(m_rotationValue.x, m_rotationValue.y, 0f);
    }

    private void followcam(){
        transform.position = PlayerMng.Instance.transform.position + new Vector3(0, 5, -4);
    }
}
