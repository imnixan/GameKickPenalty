using UnityEngine;

namespace MonoComponents
{
    public class Rotator : MonoBehaviour
    {
        private void Update()
        {
            var currentRotation = transform.rotation.eulerAngles;
            currentRotation.z += 50f * Time.deltaTime;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }
}
