using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    TextMeshPro textMesh;
    private float dissapearTimer;
    private float dissapearSpeed;
    private float moveYSpeed = 2.75f;
    private Color textColor;
    private Color originalColor;
    private Vector3 originalLocalScale;

    private const float DISSAPEAR_TIMER_MAX = 0.25f;
   


    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        originalColor = textMesh.color;
        originalLocalScale = transform.localScale;
    }
    public void Setup(int damage)
    {
        Debug.Log(textMesh);
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMeshPro>();
        }

        textMesh.material.renderQueue = 9999;
        textMesh.SetText(damage.ToString());
        dissapearTimer = DISSAPEAR_TIMER_MAX;
        dissapearSpeed = 4f;
        textColor = originalColor;
        textMesh.color = originalColor;
        transform.localScale = originalLocalScale;
    }

    public static DamagePopup ShowDamage(int damage, Vector3 position)
    {
        Debug.Log(damage);
        GameObject instance = AssetManager.Instance.DamagePopupPool.GetItemInstance();
        instance.transform.position = position;

        DamagePopup damagePopup = instance.GetComponent<DamagePopup>();
        damagePopup.Setup(damage);

        return damagePopup;
    }

    private void Update()
    {
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        if (dissapearTimer >= DISSAPEAR_TIMER_MAX * 0.5f)
        {
            float increaseSpeed = 2f;
            transform.localScale += Vector3.one * increaseSpeed * Time.deltaTime;
        } else
        {
            float decreaseSpeed = 1f;
            transform.localScale -= Vector3.one * decreaseSpeed * Time.deltaTime;
        }
        dissapearTimer -= Time.deltaTime;
        if (dissapearTimer <= 0)
        {
            textColor.a -= dissapearSpeed * Time.deltaTime;
            textMesh.color = textColor;
        }

        if (textColor.a <= 0)
        {
            gameObject.SetActive(false);
            AssetManager.Instance.DamagePopupPool.AddToPool(gameObject);
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
