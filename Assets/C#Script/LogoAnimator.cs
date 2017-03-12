using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogoAnimator : MonoBehaviour {

    public bool glowingBool = false;

	private Image image;
	private Color originalColor;

	void Start()
	{
		image = GetComponent<Image>();
		originalColor = image.color;
	}

    void Update()
    {
        if (glowingBool == false)
        {
            StartCoroutine(Glowing());
            glowingBool = true;
        }
    }
		
	IEnumerator Glowing()
	{
		float pulseTime = 3.0f;
		float counter = 0.0f;
		float percent;
		bool increase = false;

		while (gameObject != null)
		{
			if (counter >= pulseTime)
			{
				increase = false;
			}
			else if (counter <= 0)
			{
				increase = true;
			}

			if (increase == true)
				counter += Time.deltaTime;
			else if (increase == false)
				counter -= Time.deltaTime;

			percent = counter / pulseTime;

			image.color = Color.Lerp(originalColor, Color.black, percent);

			yield return null;
		}
	}
}
