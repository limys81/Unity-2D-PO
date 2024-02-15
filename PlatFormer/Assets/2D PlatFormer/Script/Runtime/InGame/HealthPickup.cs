using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestroe = 20;
    public Vector3 spinRotation = new Vector3(0, 180, 0);

    AudioSource pickupSource;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            bool wasHealed = damageable.Heal(healthRestroe);
            if (wasHealed)
            {
                if (pickupSource)
                {
                    AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
                }

                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotation * Time.deltaTime;
    }
}
