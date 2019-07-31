//
// Unityちゃん用の三人称カメラ
// 
// 2013/06/07 N.Kobyasahi
//
using UnityEngine;
using System.Collections;

namespace UnityChan
{
	public class ThirdPersonCamera : MonoBehaviour
	{
		public float smooth = 3f;		// カメラモーションのスムーズ化用変数
        [SerializeField] Vector3 cameraOffset;
        [SerializeField] Player localPlayer;
        [SerializeField] Transform cameraLookTarget;
        [SerializeField] Transform aimingLookTarget;
        [SerializeField] GameObject crosshair;

        public GameObject crosshairObject;

        public float sensitivityVert;
        public float minimumVert = -45f;
        public float maximumVert = 45f;
        public float _rotationX;
        private float aimingXOffset = 0.5f;
        private Vector3 targetPosition;
        private Quaternion targetRotation;

		Transform standardPos;
        

        
	
		void Start ()
		{
			
		}

        private void Update()
        {
            if (Input.GetButton("Fire2"))
            {
                if(crosshairObject == null)
                    crosshairObject = Instantiate(crosshair);

                targetPosition = cameraLookTarget.position + localPlayer.transform.forward * (cameraOffset.z / 2) +
                    localPlayer.transform.right * aimingXOffset;


                targetRotation = Quaternion.LookRotation(aimingLookTarget.position - targetPosition, Vector3.up);

            }

            else
            {
                if (crosshairObject != null)
                    Destroy(crosshairObject);

                targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraOffset.z +
                    localPlayer.transform.up * cameraOffset.y +
                    localPlayer.transform.right * cameraOffset.x;
                targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);
            }

            
            transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smooth * Time.deltaTime);

            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            Vector3 rotation = transform.localEulerAngles;
            rotation.x = _rotationX;

            transform.localEulerAngles = rotation;
        }



    }
}