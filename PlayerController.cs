using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Text countText;
    public Text winText;

    public float fMoveSpeed = 0.01f;
    public float fRotSpeed = 1.2f;
    public float fJumpSpeed = 10.0f;
    public float fGravity = 20.0f;

    public Camera[] arrCam;

    int nCamCount = 3;
    int nNowCam = 0;

    CharacterController m_Ctrl = null;
    int count = 0;

    Vector3 moveDir;

    private GameObject m_objCarry = null;
    public GameObject Cam = null;


    void Start()
    {
        m_Ctrl = GetComponent<CharacterController>();

        SetCountText();

        winText.text = "";

        Cam = GameObject.FindGameObjectWithTag("Main Camera1");


        StartCoroutine(PrintRand());
        StartCoroutine(CameraShake());

    }

    IEnumerator PrintRand()
    {
        yield return null;

        Debug.Log("PrintRand start");
        int sum = 0;

        for (int i = 0; i < 4; ++i)
        {
            yield return new WaitForSeconds(0.3f);
            sum += Random.Range(0, 4);
        }

        Debug.Log(sum);
        yield break;
    }

    IEnumerator CameraShake()
    {
        yield return null;

        Cam.transform.position += Vector3.one * 100;


        yield break;
    }

	void Update ()
	{
         Debug.DrawRay(transform.position, transform.forward * 20, Color.blue);

        //1.Physics.Raycast : 처음 닿은 하나만 검출
             RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 8) == true)
        {
            Debug.Log(hit.collider.gameObject.name);
        }

        // 2. Physics.RaycastAll : 전부 검출, 순서는 보장하지 않음
        //      RaycastHit[] hits;
        // 
        //      hits = Physics.RaycastAll(transform.position, transform.forward, 8);
        // 
        //      for( int i=0; i<hits.Length; ++i )
        //      {
        //          // 블라블라
        //      }
        if ( Input.GetButtonUp("Fire3"))
		{
			++nNowCam;

			if (nNowCam >= nCamCount)
			{
				nNowCam = 0;
			}

			for ( int i=0; i<arrCam.Length; ++i )
			{
				arrCam[i].enabled = (i == nNowCam);
			}
		}

		if( m_Ctrl.isGrounded == true )
		{
			float fRot = fRotSpeed * Time.deltaTime;

			float hor = Input.GetAxis("Horizontal");
			float ver = Input.GetAxis("Vertical");

			transform.Rotate(Vector3.up * hor * fRot);

			moveDir = new Vector3(0, 0, ver * fMoveSpeed);
			moveDir = transform.TransformDirection(moveDir); // 로컬 좌표계 -> 월드 좌표계

			if( Input.GetButton("Jump") == true )
			{
				moveDir.y = fJumpSpeed;

				if(m_objCarry != null )
				{
					m_objCarry.transform.parent = this.gameObject.transform;
					m_objCarry.GetComponent<BoxCollider>().enabled = false;
				}
			}
		}

		moveDir.y -= (fGravity * Time.deltaTime);

		m_Ctrl.Move(moveDir * Time.deltaTime); // 월드 좌표계 기준 이동위치를 넣어줌
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
		else if(other.gameObject.CompareTag("Box"))
		{
			m_objCarry = other.gameObject;
		}
		
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Box"))
		{
			m_objCarry = null;
		}
	}


	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 12) 
		{
			winText.text = "You Win!";
		}
	}
}