using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public static HealthBarManager Instance;

    public GameObject healthBarPrefab;
    public Canvas canvas;

    void Awake()
    {
        Instance = this;
    }

    public HealthBar CreateHealthBar(Transform targetTransform)
    {
        GameObject healthBarGO = Instantiate(healthBarPrefab, canvas.transform);
        HealthBar healthBar = healthBarGO.GetComponent<HealthBar>();
        healthBar.Initialize(targetTransform);
        return healthBar;
    }
}
