using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevGunController : EquippableObject, ActionableObject
{
    public GameObject simpleBulletPrefab;
    public Vector3 shootingPoint = new Vector3(0, 0, 0);
    public float shootingForceMagnitude = 5f;
    public float bulletsPerSecond = 1f;
    private bool isShootingMultiple = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void primaryAction_Start()
    {
        shootSingleBullet();
        //Invoke("shootSingleBullet", 1/bulletsPerSecond);
    }
    public void primaryAction_Hold()
    {
        print("taka taka taka");
    }
    public void primaryAction_Cancel()
    {
        print("Shooting stopped");
    }
    public void secondaryAction_Start() {
        isShootingMultiple = true;
        shootMultipleBullets();
    }
    public void secondaryAction_Hold() {}
    public void secondaryAction_Cancel() {
        isShootingMultiple = false;
    }
    private void shootSingleBullet() {
        GameObject newBullet = Instantiate(simpleBulletPrefab, this.transform.position, this.transform.rotation, this.transform);
        newBullet.transform.localPosition = shootingPoint;
        newBullet.transform.parent = null;
        newBullet.GetComponent<Rigidbody>().velocity = this.transform.forward * shootingForceMagnitude;
    }
    private void shootMultipleBullets() {
        shootSingleBullet();
        if(isShootingMultiple) Invoke("shootMultipleBullets", 1/bulletsPerSecond);
    }
}
