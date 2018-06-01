using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Canvas))]
public class UserInterface : MonoBehaviour {
    public Health healthData;
    public Image healthbar;
    public Text damageText;
    public Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthbar.fillAmount = healthData.current / healthData.maximum;
    }

    public void DisplayDamagePopup(Character character, float damage)
    {
        Text dText = Instantiate(damageText, canvas.transform);
        dText.transform.position = Camera.main.WorldToScreenPoint(character.transform.position);
        dText.transform.position += new Vector3(Random.Range(-15, 15), Random.Range(-15, 15));
        dText.text = damage.ToString();
        StartCoroutine(DestroyAfter(dText.gameObject, 1f));
    }

    public IEnumerator DestroyAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}