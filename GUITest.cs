using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GLOBAL;

namespace GLOBAL
{
    public static class Path
    {
        public static string PREFAB_PATH_ADD = "Prefab/{0}";
        //public static string PREFAB_PATH_ADD2 = "Prefab\\{0}";
    }
}

public static class ExtensionMethod
{
    // GameObject
    public static GameObject Instantiate_asChild(this GameObject a_objParent, string a_strPrefabName)
    {
        if (string.IsNullOrEmpty(a_strPrefabName) == true)
        {
            Debug.LogError("arg error");
            return null;
        }

        string FileName_withPath = string.Format(Path.PREFAB_PATH_ADD, a_strPrefabName);

        GameObject objPrefab = Resources.Load(FileName_withPath) as GameObject;

        if (objPrefab == null)
        {
            Debug.LogError("logic error");
            return null;
        }

        return a_objParent.Instantiate_asChild(objPrefab);
    }

    public static GameObject Instantiate_asChild(this GameObject a_objParent, GameObject a_objPrefab)
    {
        GameObject objChild = GameObject.Instantiate(a_objPrefab) as GameObject;

        if (objChild != null)
        {
            objChild.transform.parent = a_objParent.transform;
            objChild.transform.localPosition = Vector3.zero;
            objChild.transform.localRotation = Quaternion.identity;
            objChild.transform.localScale = Vector3.one;
            objChild.layer = a_objParent.layer;
        }

        return objChild;
    }
}

    public class GUITest : MonoBehaviour
{
	#region INSPECTOR

	public GameObject m_obj;

    #endregion


    private bool Checkbullet = true;

	string m_strState = string.Empty;

	private void OnGUI()
	{
		if( GUI.RepeatButton(new Rect(10, 240, 50, 30), "<") == true )
		{
			m_strState = "왼쪽 클릭";
			m_obj.transform.Translate(Vector3.left / 100);
		}

		if (GUI.RepeatButton(new Rect(70, 240, 50, 30), ">") == true)
		{
			m_strState = "오른쪽 클릭";
			m_obj.transform.Translate(Vector3.right / 100);
		}

		if (GUI.RepeatButton(new Rect(400, 240, 50, 30), "Fire") == true && Checkbullet == true)
		{
			m_strState = "총알 발사!";

            var temp = Time.deltaTime;

            Checkbullet = false;

            //GameObject b = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            GameObject b = gameObject.Instantiate_asChild("Cube");

			b.transform.parent = m_obj.transform;
			b.transform.localPosition = Vector3.zero;
			b.transform.localScale = new Vector3(1, 1, 1);
			b.name = "bullet";
			b.AddComponent<TestBullet>();

            StartCoroutine(Check1());

		}

		GUILayout.Label(m_strState);
	}

    IEnumerator Check1()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            Checkbullet = true;
        }
    }


}
