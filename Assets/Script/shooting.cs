using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shooting : MonoBehaviour
{
    //Gun stats
    public float damage, startTimerBetweenShooting,timebetweenShots, spread, range, reloadTime;
    public int magazineSize;
    public int bulletsLeft, bulletShot, bulletsPerTap;

    public Camera fpsCam;
    public Transform attackpoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    //public Camshake camshake;
    public GameObject muzzleFlash, bulletHoleGraphic;
    public float camShakeMagnitude, camShakeDuration;


    private bool shoot_flag, readToShoot, reloading;
    public bool allowButtonHold;
    //Show bullet amount
    public TextMeshPro showamount;

    // Start is called before the first frame update
    void Awake()
    {
        bulletsLeft = magazineSize;
        readToShoot = true;

    }

    

    // Update is called once per frame
    void Update()
    {
        MyInput();

        showamount.SetText(bulletsLeft + "/" + magazineSize);
    }

    private void MyInput()
    {
        //Input
        if (allowButtonHold) shoot_flag = Input.GetKey(KeyCode.Mouse0);
        else shoot_flag = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();


        if (readToShoot && shoot_flag && !reloading && bulletsLeft > 0) {
            bulletShot = bulletsPerTap;
            Shoot();
        
        } 
        
    }

    private void Shoot()
    {
        readToShoot = false;
        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // 만약 rigidbody를쓰고싶으면 
        //if (Rigidbody.velocity.magnitude > 0) { spread = spread * 1.5f}
        //else spread = "normal speed";

        //방향과 속도계산
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);


        //RayCast
        if(Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.gameObject.name);
            if (rayHit.collider.CompareTag("Enemy"))
            {
                //TakeDamage
                //rayHit.collider.GetComponent<T>().takeDamage(damgae);
            }
        }


        //이걸쓰나보네 
        //camShake.Shake(camShakeDuration, camShakeMagnitude);
        //Instantiate Bullet graphic
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackpoint.position, Quaternion.identity);
       

        bulletsLeft--;
        bulletShot--;
        Invoke("ResetShot", startTimerBetweenShooting);

        if(bulletShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timebetweenShots);
        }
    }

    private void Reload()
    {
        reloading = true;

        Invoke("ReloadingFinished", reloadTime);
    }

    private void ReloadingFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

}
