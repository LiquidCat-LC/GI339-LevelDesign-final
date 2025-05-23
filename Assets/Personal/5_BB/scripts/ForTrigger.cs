using UHFPS.Runtime;
using UnityEngine;
using System.Collections;

public class ForTrigger : MonoBehaviour
{
    public JumpscareManager jumpscareManager;
    public GameObject jumpscareObj;
    public GameObject NowJumpObj;
    
    public bool Istrigger;

    bool hasInstantiated = false;
    bool duringJump;
    public GameObject player;
    private PlayerStateMachine playerStateMachine;
    float newWalkSpeed = 1f;
    float oldWalkSpeed = 3f;
    float newRunSpeed = 1f;
    float oldRunSpeed = 3f;
    public GameObject[] InvisibleWall;
    public bool FirstJump;
    public GameObject Tutorial;
    public bool isSafeZone;
    
    private void Awake()
    {
        jumpscareManager = JumpscareManager.Instance;
        playerStateMachine = player.GetComponent<PlayerStateMachine>();
        FirstJump = true ;
        isSafeZone = false;
    }
    public void Update()
    {
        if(!isSafeZone)
        {
            if(Istrigger == true && !hasInstantiated )
            {
                if(FirstJump)
                {
                    Tutorial.gameObject.SetActive(true);
                }
                NowJumpObj = Instantiate(jumpscareObj, transform.position, transform.rotation);
                hasInstantiated = true;
                duringJump = true;
                playerStateMachine.PlayerBasicSettings.WalkSpeed = newWalkSpeed;
                playerStateMachine.PlayerBasicSettings.RunSpeed = newRunSpeed;
            }
            else if(Istrigger == false && duringJump == true)
            {
                if(FirstJump)
                {
                    Tutorial.gameObject.SetActive(false);
                    FirstJump = false;
                }
                playerStateMachine.PlayerBasicSettings.WalkSpeed = oldWalkSpeed;
                playerStateMachine.PlayerBasicSettings.RunSpeed = oldRunSpeed;
                StartCoroutine(Wait(5f));
            }
        }
        else
        {
            if(NowJumpObj != null)
            {
                StartCoroutine(Wait(5f));
                playerStateMachine.PlayerBasicSettings.WalkSpeed = oldWalkSpeed;
                playerStateMachine.PlayerBasicSettings.RunSpeed = oldRunSpeed;
            }
        }
        
    }

    IEnumerator Wait(float waitTime)
    {
        jumpscareManager.EndJumpscareEffectForVFX();
        
        yield return new WaitForSeconds(waitTime);

        duringJump = false;
        hasInstantiated = false;
        Destroy(NowJumpObj);
    }

    public void SafeZoneCheck()
    {
        isSafeZone = true;
    }

}
