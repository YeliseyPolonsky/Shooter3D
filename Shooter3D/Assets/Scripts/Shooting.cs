using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float _damage = 20f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private GameObject flash;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bulletMark;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private AudioSource bulletSound;

    private float timer;
    private Coroutine shootProcess;

    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && timer >= fireRate)
        {
            timer = 0;
            Shot();
        }
    }

    private void Shot()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, layerMask))
        {
            if (hit.collider.GetComponent<EnemyBodyPart>() is EnemyBodyPart enemyBodyPart)
            {
                enemyBodyPart.Hit(_damage, enemyBodyPart, ray.direction);
            }

            GameObject newBulletMark = Instantiate(bulletMark, hit.point, Quaternion.LookRotation(hit.normal));
            newBulletMark.transform.parent = hit.transform;
        }

        animator.SetTrigger("Shot");
        bulletSound.Stop();
        bulletSound.Play();

        if (shootProcess != null)
        {
            StopCoroutine(shootProcess);
        }

        shootProcess = StartCoroutine(ShootProcess());
    }

    private IEnumerator ShootProcess()
    {
        flash.transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);
        flash.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        flash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        flash.SetActive(false);
    }
}