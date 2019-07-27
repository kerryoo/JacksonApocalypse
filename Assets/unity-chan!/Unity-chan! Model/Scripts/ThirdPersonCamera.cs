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

        public float sensitivityVert = 1f;
        public float minimumVert = -45f;
        public float maximumVert = 45f;
        public float _rotationX;

		Transform standardPos;
        

        
	
		void Start ()
		{
			
		}

        private void Update()
        {
            Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraOffset.z +
                localPlayer.transform.up * cameraOffset.y +
                localPlayer.transform.right * cameraOffset.x;

            Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smooth * Time.deltaTime);

            //_rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert * Time.deltaTime;
            //_rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            //Vector3 rotation = transform.localEulerAngles;
            //rotation.x = _rotationX;

            //transform.localEulerAngles = rotation;
        }

       

	}
}