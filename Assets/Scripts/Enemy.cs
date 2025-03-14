using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public ContactPoint2D[] listContacts = new ContactPoint2D[1]; // contact colision 

    private void OnCollisionEnter2D(Collision2D other) {
        // Vérifie si l'objet qui entre en collision est le joueur (grâce au tag "Player")
        if (other.gameObject.CompareTag("Player")) {  
            // Récupère le(s) point(s) de contact de la collision et les stocke dans listContacts
            other.GetContacts(listContacts);

            // le joueur atterrit sur l'ennemi
            if (listContacts[0].normal.y < -0.5f) { 
                Destroy(gameObject); //l'ennemi est détruit
            } else {
                // Récupère le composant "PlayerHealth" du joueur pour gérer ses points de vie
                PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>(); 
                playerHealth.Hurt();  
            }
        }
    }
}