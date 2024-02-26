using UnityEngine;

namespace Services.LoadingScreenService
{
    public class Opener : MonoBehaviour
    {
        private void Awake()
        {
            EnableCurtain(true);

            DontDestroyOnLoad(this);
        }

        public void Show() => EnableCurtain(true);

        public void Hide() => gameObject.SetActive(false);

        private void EnableCurtain(bool enable) => gameObject.SetActive(enable);
    }
}
