using UnityEngine;

namespace Services.ScreenOrientationService
{
    public class RotationFinder
    {
        public void EnablePortrait()
        {
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;

            Screen.orientation = ScreenOrientation.Portrait;
        }

        public void DisablePortrait()
        {
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;

            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
}
