using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunScript : MonoBehaviour
{
    [Range(0.0f, 360.0f)]
    public float rotateBy = 0f;

    public InputActionReference controllerActionTrigger;

    public XRBaseController controller;
    public float amplitude = 0.5f;
    public float duration = 0.5f;

    public Transform raycastOrigin;
    public float gunRange = 50f;
    public float laserDuration = 0.05f;
    public float fireRate = 0.5f;
    LineRenderer laserLine;
    float fireTimer;

    AudioSource gunAudio;

    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (controllerActionTrigger.action.ReadValue<float>() != 0 && fireTimer > fireRate)
        {
            Debug.Log("Trigger from " + gameObject.name.ToString());
            fireTimer = 0;
            fireGun();
        }
    }

    // FireGun is called when gun is fired
    void fireGun()
    {
        Debug.Log("Gun was fired");

        controller.SendHapticImpulse(amplitude, duration);
        gunAudio.PlayOneShot(gunAudio.clip);

        laserLine.SetPosition(0, raycastOrigin.position);
        RaycastHit hit;
        int layerMask = 1 << 2;
        layerMask = ~layerMask;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, gunRange, layerMask))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.tag == "Target")
            {
                hitObject.SendMessage("Shatter", hit.point, SendMessageOptions.DontRequireReceiver);
                Score.instance.AddPoint();
                Debug.Log("I've hit the target!");
                laserLine.SetPosition(1, hit.point);
            }
            else
            {
                laserLine.SetPosition(1, hit.point);
            }
        } 
        else
        {
            laserLine.SetPosition(1, raycastOrigin.position + (raycastOrigin.forward * gunRange));
        }
        StartCoroutine(ShootLaser());
    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}
