using UnityEngine;
using UnityEngine.UI;

public class DemoTimer : MonoBehaviour
{
    public static DemoTimer Instance;

    [SerializeField] private float maxTime_ = 60f;
    [SerializeField] private Text timerText_;
    [SerializeField] private Slider timerSlider_;
    [SerializeField] private bool startOnAwake_ = true;

    private float currentTime_;
    private bool isRunning_;

    public float GetCurrentTime {  get { return currentTime_; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        currentTime_ = maxTime_;

        if (timerSlider_ != null)
        {
            timerSlider_.minValue = 0f;
            timerSlider_.maxValue = 1f;
            timerSlider_.value = 1f;
        }

        isRunning_ = startOnAwake_;
        UpdateView();
    }

    private void Update()
    {
        if (!isRunning_) return;

        currentTime_ = Mathf.Max(currentTime_ - Time.deltaTime, 0f);
        UpdateView();

        if (currentTime_ <= 0f)
        {
            isRunning_ = false;
        }
    }

    public void StartTimer()
    {
        isRunning_ = true;
    }

    public void StopTimer()
    {
        isRunning_ = false;
    }

    public void ResetTimer()
    {
        currentTime_ = maxTime_;
        UpdateView();
    }

    private void UpdateView()
    {
        if (timerText_ != null)
        {
            timerText_.text = $"Limit{((int)currentTime_ / 60)}:{((int)currentTime_ % 60).ToString("00")}";
        }

        if (timerSlider_ != null)
        {
            timerSlider_.value = currentTime_ / maxTime_;
        }
    }

    private void OnTimeUp()
    {

    }
}