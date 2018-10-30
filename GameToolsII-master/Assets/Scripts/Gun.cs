using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private GameObject m_BulletPrefab;
    [SerializeField] private Transform m_bulletReference;
    [SerializeField] private Character m_character;

    private void OnEnable()
    {
        m_character.OnFire.AddListener(Fire);
    }

    private void OnDisable()
    {
        m_character.OnFire.RemoveListener(Fire);
    }

    private void Fire()
    {
        Instantiate(m_BulletPrefab, m_bulletReference.transform.position, m_bulletReference.rotation);
    }
}
