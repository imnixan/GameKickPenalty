using UnityEngine;

namespace Services.ScreenOrientationService
{
    public class SizeFinder
    {
        private Rect _rect;

        public Rect Resize(ScreenOrientation orientation)
        {
            float x = Screen.safeArea.x;
            float y = Screen.safeArea.y;
            int width = Screen.width;
            int height = Screen.height;

            if (height > width)
            {
                Resize(0, y + 50f, width, height - y);
            }
            else
            {
                if (
                    orientation == ScreenOrientation.LandscapeRight
                    || orientation == ScreenOrientation.LandscapeLeft
                )
                {
                    Resize(0, 0, width - y, height + 115);
                }
                else
                {
                    Resize(y + 50f, 0, width - y, height);
                }
            }

            return _rect;
        }

        private void Resize(float x, float y, float width, float height)
        {
            _rect.x = x;
            _rect.y = y;
            _rect.width = width;
            _rect.height = height - 115;
        }
    }
}
