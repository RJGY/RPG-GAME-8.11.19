using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialHealth : MonoBehaviour
{
    public Image radialHealthIcon;
    public float currentHealth, maxHealth;
    // Start is called before the first frame update
    private void Start()
    {
        radialHealthIcon = GetComponent<Image>();
    }
    void HealthChange()
    {
        float amount = Mathf.Clamp01(currentHealth / maxHealth);
        radialHealthIcon.fillAmount = amount;
    }

    // Update is called once per frame
    void Update()
    {
        HealthChange();
    }
}
