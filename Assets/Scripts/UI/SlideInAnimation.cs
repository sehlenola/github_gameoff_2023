using UnityEngine;

public class SlideInAnimation : MonoBehaviour
{
    public Vector3 hiddenPosition;
    public Vector3 shownPosition;
    public float duration = 1f;

    private float timer;
    private bool isAnimating;

    public void AnimateIn()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, hiddenPosition.y, transform.localPosition.z); ;
        shownPosition = new Vector3(transform.localPosition.x, shownPosition.y, transform.localPosition.z);
        isAnimating = true;
        timer = 0f;
    }

    void Update()
    {
        if (isAnimating)
        {
            timer += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(timer / duration);
            transform.localPosition = Vector3.Lerp(hiddenPosition, shownPosition, t);

            if (timer >= duration)
            {
                isAnimating = false;
            }
        }
    }
}
