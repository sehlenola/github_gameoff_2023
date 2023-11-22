using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.5f;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float offSet = 0.5f;
    [SerializeField] private TextMeshPro damageText;
    public AnimationCurve scaleCurve;
    private Coroutine _returnToPoolTimerCoroutine;
    private void OnEnable()
    {
        transform.localScale = new Vector3(1, 1, 1);
        _returnToPoolTimerCoroutine = StartCoroutine(ReturnToPoolAfterTime());
        transform.position = transform.position + new Vector3(Random.Range(-offSet,offSet), 0, Random.Range(-offSet, offSet));
    }
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;
        float initialScale = transform.localScale.x;

        while (elapsedTime < lifeTime)
        {
            float scale = initialScale * scaleCurve.Evaluate(elapsedTime / lifeTime);
            transform.localScale = new Vector3(scale, scale, scale);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    public void SetDamage(float damage)
    {
        damageText.text = damage.ToString();
    }
}
