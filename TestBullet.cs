using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour {

	float fSpeed = 10.0f;

	private void Update()
	{
		this.transform.Translate(Vector3.forward * fSpeed * Time.deltaTime);

		if(this.transform.position.y > 30.0f )
		{
			Destroy(this.gameObject);
		}
	}
}
