using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour
{
    [SerializeField] private float spellCooldown;
    [SerializeField] private float lifetime = 10f;
    [SerializeField] private KeyCode spellButton = KeyCode.Z;
    [SerializeField] private Vector3 spellOffset;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private bool isFaded = false;
    private Image cooldownImage;

    private float currentSpellCooldown;

    private void Awake()
    {
        currentSpellCooldown = spellCooldown;
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0f;
        }
    }

    public void SetImage(Image image)
    {
        cooldownImage = image;
    }

    public void Spawn()
    {
        if (isFaded)
        {
            StartCoroutine(ChangeOpacity());
        }
        Destroy(gameObject, lifetime);
    }

    public bool IsCanSpawn()
    {
        return currentSpellCooldown <= 0f;
    }

    public void DecreaseCooldown(float time)
    {
        currentSpellCooldown -= time;

        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = (currentSpellCooldown / spellCooldown);
        }
    }

    private IEnumerator ChangeOpacity()
    {
        float currentTime = 0f;
        var material = GetComponent<MeshRenderer>().material;

        Color initialColor = material.color;
        initialColor.a = 0f;
        material.color = initialColor;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, currentTime / fadeDuration);
            Color newColor = material.color;
            newColor.a = alpha;
            material.color = newColor;

            yield return null;
        }

        Color finalColor = material.color;
        finalColor.a = 1f;
        material.color = finalColor;

        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void ResetCooldown()
    {
        currentSpellCooldown = spellCooldown;

        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0f;
        }
    }

    public KeyCode GetSpellButton() => spellButton;
    public Vector3 GetSpellOffset() => spellOffset;
}