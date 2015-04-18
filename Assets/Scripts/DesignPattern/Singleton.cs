using UnityEngine;
using System.Collections;

namespace DesignPattern
{

	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;
		private static object _lock = new object();
		private static bool applicationIsQuitting = false;

		public static T Instance
		{
			get
			{
				if(applicationIsQuitting)
				{
					Debug.LogWarning("[Singleton] Instance '"+ typeof(T) +
						"' already destroyed on application quit." +
						" Won't create again - returning null.");
					return null;
				}
				lock(_lock)
				{
					if(_instance == null)
					{
						_instance = (T) FindObjectOfType(typeof(T));
						if(FindObjectsOfType(typeof(T)).Length > 1)
						{
							Debug.LogError("[Singleton] Something went really wrong " +
								" - there should never be more than 1 singleton!" +
								" Reopenning the scene might fix it.");
							return _instance;
						}
						if(_instance == null)
						{
							GameObject singleton = new GameObject();
							_instance = singleton.AddComponent<T>();
							singleton.name = typeof(T).ToString();
	 
							DontDestroyOnLoad(singleton);
	 					}
					}
					return _instance;
				}
			}
		}

		public void OnDestroy ()
		{
			if (!Application.isEditor)
				applicationIsQuitting = true;
		}

	}

}