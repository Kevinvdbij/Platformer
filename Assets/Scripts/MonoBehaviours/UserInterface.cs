using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Canvas))]
public class UserInterface : MonoBehaviour {
    public Health healthData;
    public Health healthDataP2;
    public Image healthbar;
    public Image healthbarP2;
    public Text damageText;
    public Canvas canvas;
    public GameObject victoryUI;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        UpdateHealthBar();
        UpdateHealthBarP2();
    }

    public void UpdateHealthBar()
    {
        healthbar.fillAmount = healthData.current / healthData.maximum;
    }

    public void UpdateHealthBarP2()
    {
        healthbarP2.fillAmount = healthDataP2.current / healthDataP2.maximum;
    }

    public void DisplayDamagePopup(Character character, float damage)
    {
        Text dText = Instantiate(damageText, canvas.transform);
        dText.transform.position = Camera.main.WorldToScreenPoint(character.transform.position);
        dText.transform.position += new Vector3(Random.Range(-15, 15), Random.Range(-15, 15));
        dText.text = damage.ToString();
        StartCoroutine(DestroyAfter(dText.gameObject, 1f));
    }

    public void SetVictoryUI(bool state)
    {
        victoryUI.SetActive(state);
    }

    public IEnumerator DestroyAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}