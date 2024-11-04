using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour
{
    [SerializeField] private float spellCooldown;
    [SerializeField] private float lifetime = 10f;
    [SerializeField] private KeyCode spellButton = KeyCode.Z;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float distance = 1f;
    [SerializeField] private AudioSource audioSource;

    [field: SerializeField] public ESpellType SpellType { get; private set; } = ESpellType.Object;
    [field: SerializeField] public bool IsFaded { get; private set; } = false;
    private Image cooldownImage;

    private float currentSpellCooldown;

    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        if (audioSource != null )
        {
            audioSource.volume = BackgroundMusic.Instance.Volume;
        }
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
        if (IsFaded)
        {
            StartCoroutine(ChangeOpacity());
        }
        Destroy(gameObject, lifetime);
    }

    public void MoveObstacle(Transform block, Vector3 direction)
    {
        StartCoroutine(MoveOverTime(block, direction));
    }

    private IEnumerator MoveOverTime(Transform block, Vector3 direction)
    {
        Vector3 startPosition = block.position;
        Vector3 targetPosition = startPosition + direction * distance;
        float elapsedTime = 0;
        float totalTime = 1.0f;

        while (elapsedTime < totalTime)
        {
            block.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / totalTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        block.position = targetPosition;
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
        var material = GetComponentInChildren<MeshRenderer>().material;

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
}