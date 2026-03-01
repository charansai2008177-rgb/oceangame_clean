using UnityEngine;
using UnityEngine.Events;

namespace AIPackage
{
	public class DayAndNightControl : MonoBehaviour
	{
		public bool StartDay; //start game as day time
		public GameObject StarDome;
		public GameObject moonState;
		public GameObject moon;
		public Color[] dawnColors;
		public Color[] dayColors;
		public Color[] nightColors;
		public int currentDay = 0;
		public Light directionalLight; //the directional light in the scene we're going to work with
		public float SecondsInAFullDay = 120f; //in realtime, this is about two minutes by default. (every 1 minute/60 seconds is day in game)
		[Range(0, 1)]
		public float currentTime = 0; //at default when you press play, it will be nightTime. (0 = night, 1 = day)
		[HideInInspector]
		public float timeMultiplier = 1f; //how fast the day goes by regardless of the secondsInAFullDay var. lower values will make the days go by longer, while higher values make it go faster. This may be useful if you're siumulating seasons where daylight and night times are altered.
		public bool showUI;
		float lightIntensity; //static variable to see what the current light's insensity is in the inspector
		Material starMat;

		private bool dayCalled = false;
		private bool nightCalled = false;
		
		void Start()
		{
			lightIntensity = directionalLight.intensity; 
			starMat = StarDome.GetComponentInChildren<MeshRenderer>().material;
			if (StartDay)
			{
				currentTime = 0.3f; 
				starMat.color = new Color(1f, 1f, 1f, 0f);
			}
		}

		// Update is called once per frame
		void Update()
		{
			UpdateTime();
		}

        private void FixedUpdate()
        {
			UpdateDome();
			UpdateSkyColors();
		}

        void UpdateDome()
        {
			StarDome.transform.Rotate(new Vector3(0, 2f * Time.deltaTime, 0));
			moon.transform.LookAt(Camera.main.transform);
			directionalLight.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 90, 170, 0);
			moonState.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 100, 170, 0);
		}

		void UpdateSkyColors()
		{
			float intensityMultiplier = 1;
			if (currentTime <= 0.23f || currentTime >= 0.75f)
			{
				intensityMultiplier = 0;
				starMat.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, Time.deltaTime));
				skyColors.Invoke(nightColors);
			}
			else if (currentTime <= 0.25f)
			{
				intensityMultiplier = Mathf.Clamp01((currentTime - 0.23f) * (1 / 0.02f));
				starMat.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, Time.deltaTime));
				skyColors.Invoke(dawnColors);
			}
			else if (currentTime <= 0.73f)
			{
				intensityMultiplier = Mathf.Clamp01(1 - ((currentTime - 0.73f) * (1 / 0.02f)));
				skyColors.Invoke(dayColors);
			}

			directionalLight.intensity = lightIntensity * intensityMultiplier;
		}

		public UnityEvent<Color[]> skyColors;

		void UpdateTime()
        {
			currentTime += (Time.deltaTime / SecondsInAFullDay) * timeMultiplier;
			ResetHandlers();
			CallHandlers();
		}

		void ResetHandlers()
        {
			if (currentTime >= 1)
			{
				currentTime = 0;
				dayCalled = false;
				nightCalled = false;
			}
		}

		void CallHandlers()
        {
			if (currentTime < 0.5f && currentTime > 0.3f)
			{
				CallDayHandler();
			}
			if (currentTime > 0.7f)
			{
				CallNightHandler();
			}
		}

		public delegate void OnMorningListener();
		public event OnMorningListener OnMorningHandler;

		void CallDayHandler()
        {
			if (dayCalled == false)
            {
				OnMorningHandler?.Invoke();
				dayCalled = true;
			}
        }

		public delegate void OnEveningListener();
		public event OnEveningListener OnEveningHandler;

		void CallNightHandler()
        {
			if (nightCalled == false)
            {
				OnEveningHandler?.Invoke();
				nightCalled = true;
			}
        }

		public string TimeOfDay()
		{
			string dayState = "";
			if (currentTime > 0f && currentTime < 0.3f)
			{
				dayState = "Midnight";
			}
			else if (currentTime < 0.5f && currentTime > 0.3f)
			{
				dayState = "Morning";
			}
			else if (currentTime > 0.5f && currentTime < 0.6f)
			{
				dayState = "Mid Noon";
			}
			else if (currentTime > 0.7f && currentTime < 0.8f)
			{
				dayState = "Evening";
			}
			else if (currentTime > 0.8f && currentTime < 1f)
			{
				dayState = "Night";
			}

			return dayState;
		}
	}
}
