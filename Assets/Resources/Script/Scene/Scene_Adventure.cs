using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 도착이후 회전값 주기 

public class Scene_Adventure : SceneManager
{
    //private Transform mPlayer;
    private Transform mMouse;
    private Vector2 mKey_Input;
    private NavMeshAgent mAngent;
    public Vector3 mAngle;
    
    protected override void Awake()
    {
        base.Awake();
        Awake_Object();
        Custom.Init(this.mPlayer);
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        CharacterNavi();
    }

    private void LateUpdate()
    {
        CameraFollow();
    }

    private void Awake_Object()
    {
        mPlayer = GameObject.Find("Player").transform;
        mAngle = Camera.main.transform.position;
        mAngent = mPlayer.GetComponent<NavMeshAgent>();
    }

    private void CameraFollow()
    {
        Camera.main.transform.LookAt(mPlayer);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, mPlayer.localPosition + mAngle, 1 * Time.deltaTime);

        if (Input.GetKey(KeyCode.P) && mAngle.y >= 1)
        {
            mAngle.y -= 0.1f;
        }
        else if (Input.GetKey(KeyCode.O) && mAngle.y <= 7f)
        {
            mAngle.y += 0.1f;
        }

        // 좌우 회전 
        if (!Input.GetKey(KeyCode.Mouse1)) { return; }
        mKey_Input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (0.5f < mKey_Input.x)
        {
            mAngle.z = 4;
        }
        else if (-0.5f > mKey_Input.x)
        {
            mAngle.z = -4;
        }

        if (0.5f < mKey_Input.y)
        {
            mAngle.x = -4;
        }
        else if (-0.5f > mKey_Input.y)
        {
            mAngle.x = 4;
        }
        
    }

    private Transform MouseHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        if (hit.collider != null)
        {
            return hit.transform;
        } else
        {
            return null;
        }
    }

    private void CharacterNavi()
    {
        mMouse = MouseHit();
        if (mMouse != null)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                mAngent.SetDestination(mMouse.position);
            }
        }
    }

}
