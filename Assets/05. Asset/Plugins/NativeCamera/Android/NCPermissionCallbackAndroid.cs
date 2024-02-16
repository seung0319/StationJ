#if UNITY_EDITOR || UNITY_ANDROID
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace NativeCameraNamespace
{
	public class NCPermissionCallbackAndroid : AndroidJavaProxy
	{
		private object threadLock;
		public int Result { get; private set; }

		public NCPermissionCallbackAndroid( object threadLock ) : base( "com.yasirkula.unity.NativeCameraPermissionReceiver" )
		{
			Result = -1;
			this.threadLock = threadLock;
		}

		public void OnPermissionResult( int result )
		{
			Result = result;

			lock( threadLock )
			{
				Monitor.Pulse( threadLock );
			}
		}
	}
	public class ButtonClick : MonoBehaviour
	{
		public Button yourButton;

         void Start()
        {
            
			yourButton.onClick.AddListener(TaskOnClick);
        }

		void TaskOnClick()
		{
			object threadLock = new object();
			NCPermissionCallbackAndroid callback = new NCPermissionCallbackAndroid( threadLock );
            
			using (AndroidJavaClass nativeCamera = new AndroidJavaClass("com.yasirkula.unity.NativeCamera"))
            {
                nativeCamera.CallStatic("RequestPermission", callback);
            }

            lock (threadLock)
            {
                Monitor.Wait(threadLock);
            }

            // callback.Result를 이용하여 권한 요청 결과를 처리합니다.
            if (callback.Result == 1)
            {
                Debug.Log("Permission granted");
            }
            else
            {
                Debug.Log("Permission denied");
            }
        } 


    }
	
}
#endif