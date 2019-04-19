using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageRedScreen : MonoBehaviour
{
    public Image redScreen;
    public float decayRate;

    void Start() {
        redScreen = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        decayColor();
    }

    void decayColor() {
        if(redScreen.color.a > 0) {
            Color newColor = redScreen.color;
            newColor.a = Mathf.Clamp(newColor.a - (decayRate * Time.deltaTime / 255), 0, 1);
            redScreen.color = newColor;
        }
    }

    public void flashOnDamage(int damage) {
        Color newColor = redScreen.color;
        print(newColor);
        newColor.a = Mathf.Clamp(newColor.a + ((float)damage / 255), 0, 1);
        print(newColor);
        redScreen.color = newColor;
    }
}
