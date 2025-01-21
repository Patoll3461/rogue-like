using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarFill; // Assign in the inspector
    [SerializeField] private Transform target; // The object to follow
    private Vector3 offset = new Vector3(0, 1.5f, 0); // Adjust this as needed

    public void Initialize(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Update()
    {
        if (target != null)
        {
            // Update the position of the health bar
            transform.position = target.position + offset;
        }
    }

    public void SetHealth(float healthPercentage)
    {
        healthBarFill.fillAmount = healthPercentage;
        //Debug.Log(healthBarFill.fillAmount);
    }
}
