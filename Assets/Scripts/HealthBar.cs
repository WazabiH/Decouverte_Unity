using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage; // Image de la barre de vie
    public PlayerData dataPlayer; // Données du joueur (points de vie)
    public Gradient lifeColorGradient; // Dégradé de couleurs de la barre de vie

    void Update()
    {
        // Calcul du ratio de vie et mise à jour de la barre
        float lifeRatio = (float)dataPlayer.currentLifePoints / (float)dataPlayer.maxLifePoints;
        fillImage.fillAmount = lifeRatio;

        // Changement de couleur de la barre selon le ratio de vie
        fillImage.color = lifeColorGradient.Evaluate(lifeRatio);
    }
}
