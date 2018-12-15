using System.Collections;
     using System.Collections.Generic;
     using UnityEngine;
     using UnityEngine.SceneManagement;
     
     public class Manager : MonoBehaviour
     {
         [SerializeField] private float m_PlayerHealth;
     
         private void Start()
         {
     
         }
     
         public void DecreaseHealth()
         {
             m_PlayerHealth--;
         }
     
         private void CheckHealth()
         {
             if (m_PlayerHealth <= 0)
             {
                 GameOver();
             }
         }
    private void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
