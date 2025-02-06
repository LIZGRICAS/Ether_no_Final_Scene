using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider;

    void Start()
    {
        
        // Check if the Canvas or TextMeshProUGUI component is active before initializing
        if (slider != null && slider.gameObject.activeInHierarchy)
        {
            // Initialize the text with the starting number of lives
            slider = GetComponent<Slider>();
        }
        else
        {
            Debug.LogWarning("slider component is not active in the hierarchy.");
        }
    }

    public void ChangeLifeMaximum(float lifeMax)
    {
        slider.maxValue = lifeMax;
    }
    
    public void ChangeLifeCurrent(float lifeCant)
    {
        slider.value = lifeCant;
    }
    
    public void InitializeLifeBar(float lifeCant)
    {
        
        print("Value of health: " + lifeCant );
        // ChangeLifeMaximum(100);
        ChangeLifeCurrent(lifeCant);
    }

}
