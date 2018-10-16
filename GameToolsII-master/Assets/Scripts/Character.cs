using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{

    private bool m_picked;
    private Animator m_animator;

    private bool m_enableIK;
    private float m_weightIK;
    private Vector3 m_positionIK;

    // Use this for initialization
    void Start()
    {
        // Initialize Animator
        m_animator = GetComponent<Animator>();
    }

    public void Move(float turn, float forward, bool jump, bool picked)
    {
        m_animator.SetFloat("Turn", turn);
        m_animator.SetFloat("Forward", forward);
        m_picked = true;
        
        if (jump)
        {
            m_animator.SetTrigger("Jump");
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.name == "Pickup_Object")
        {
            var pickable = other.GetComponent<Pickable>();

            if (m_picked && pickable != null && pickable.picked)
            {
                Transform rightHand = m_animator.GetBoneTransform(HumanBodyBones.RightHand);
                pickable.picked = true;
                Debug.Log("Picking");
                Debug.Log(m_picked);
                m_animator.SetTrigger("Pick");
                //start corroutine to update position and weight
            }
        }
    }

    private IEnumerator updateIK(Collider other)
    {
        m_enableIK = true;
        while (m_enableIK)
        {
            m_positionIK = other.transform.position;
            m_weightIK = m_animator.GetFloat("IK");
            yield return null;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (m_enableIK)
        {
            m_animator.SetIKPosition(AvatarIKGoal.RightHand, m_positionIK);
            m_animator.SetIKPositionWeight(AvatarIKGoal.RightHand, m_weightIK);
        }
    }

    public void DisableIK()
    {
        m_enableIK = false;
    }
}
