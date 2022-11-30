using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class Scanner : MonoBehaviour
{
    private ARTrackedImageManager imageManager;

    // Scriptable Objects
    [SerializeField] private CurrencyScriptableObject[] countryCurrency;

    private void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        imageManager.trackedImagesChanged += OnTrackImageChanged;
    }

    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnTrackImageChanged;
    }

    private void OnTrackImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateInfo(trackedImage);
        }
    }

    private void UpdateInfo(ARTrackedImage trackedImage)
    {
        TextMeshProUGUI currencyInfoText = trackedImage.transform.GetChild(0).GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        if (trackedImage.trackingState != TrackingState.None)
        {
            string currencyName = trackedImage.referenceImage.name;
            string currencyCountry = "";
            string currencyType = "";
            bool isCurrencyDetected = false;

            foreach (var currentCurrencyCountry in countryCurrency)
            {
                foreach (var currencyNote in currentCurrencyCountry.currencyData)
                {
                    if (currencyNote.currencyValue == currencyName)
                    {
                        currencyCountry = currencyNote.binding.countryName;
                        currencyType = currencyNote.binding.currencyType;
                        isCurrencyDetected = true;
                    }
                }
            }

            currencyInfoText.text = isCurrencyDetected ? FormatCurrencyStrings(currencyName, currencyCountry, currencyType) : "Not in Library";

            /*switch (currencyName)
            {
                case "BPAK10rupees":
                    text.text = currencyName;
                    break;
                case "F PAK10rupees":
                    text.text = currencyName;
                    break;
                default:
                    text.text = "Not in Library";
                    break;
            }*/
        }
    } // end of UpdateInfo()
    string FormatCurrencyStrings(string currencyName, string currencyCountry, string currencyType)
    {
        currencyName = currencyName.StartsWith("F") ? currencyName.Remove(0, 4) : currencyName.Remove(0, 4);
        return string.Format("{0} {1} {2}", currencyCountry, currencyName, currencyType); // string door format ({} {} {})
    }
} // end of class