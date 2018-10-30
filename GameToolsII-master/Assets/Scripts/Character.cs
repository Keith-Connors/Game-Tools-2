using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{

    public UnityEvent OnFire;


    private float deltaX;
    private Quaternion spineRotation;
    private bool m_aim;
    
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

        if (m_aim)
        {
            Vector3 rotationEuler = new Vector3(0, deltaX, 0);
            Quaternion rotationOffSet = Quaternion.Euler(rotationEuler);

            spineRotation = Quaternion.Lerp(spineRotation, spineRotation * rotationOffSet, Time.deltaTime * 50.0f);
            rotationEuler = spineRotation.eulerAngles;
            if (rotationEuler.y > 180)
            {
                rotationEuler.y -= 360;
            }

            if (rotationEuler.y < 180)
            {
                rotationEuler.y += 360;
            }

            rotationEuler.y = Mathf.Clamp(rotationEuler.y, -60.0f, +60.0f);

            m_animator.SetBoneLocalRotation(HumanBodyBones.Spine, Quaternion.Euler(rotationEuler));
        }
    }

    public void DisableIK()
    {
        m_enableIK = false;
    }

    public void AimFire(bool aimDown, bool aimHold, bool fire)
    {
        m_animator.SetBool("aim", aimHold);
       
        if (aimHold && fire)
        {
            m_animator.SetTrigger("Fire");

            if (OnFire != null)

            {
                OnFire.Invoke();
            }
        }
    }
}
