using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
	public static T AddComponent<T>(this GameObject a_objThis) where T : Component
	{
		T component = a_objThis.GetComponent<T>();

		if (component == null)
			component = a_objThis.AddComponent<T>();

		return component;
	}

	public static T AddComponent<T>(this GameObject a_objThis, Component target) where T : Component
	{
		T component = a_objThis.AddComponent<T>(target);

		return component;
	}

	public static T GetComponent<T>(this GameObject a_objThis, string name) where T : Component
	{
		var obj = a_objThis.transform.Find(name);

		if (obj == null)
		{
			return null;
		}

		T component = obj.GetComponent<T>();
		if (component == null)
		{
			Debug.LogError(string.Format("GetComponent Name Error! - {0}", name));
			return null;
		}

		return component;
	}
}
