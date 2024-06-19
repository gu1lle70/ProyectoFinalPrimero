using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeadgeClimb : MonoBehaviour
{
    public static LeadgeClimb Instance { get; private set; }

    [SerializeField] public bool canClimbLeadge = false;
    [SerializeField] public bool ledgeDetected;
    public float leadgeClimbXOffset1 = 0f;
    public float leadgeClimbYOffset1 = 0f;
    public float leadgeClimbXOffset2 = 0f;
    public float leadgeClimbYOffset2 = 0f;
    private void Update()
    {
        CheckLeadgeClimb();
    }
    private void CheckLeadgeClimb()
    {
        if (ledgeDetected && !canClimbLeadge)
        {
            canClimbLeadge = true;

            if (PlayerSprites.Instance.facingDirection == 1.0f)
            {
                PhysicsManager.Instance.ledgePos1 = new Vector2(Mathf.Floor(PhysicsManager.Instance.ledgePosBot.x + PhysicsManager.Instance.ledgeDistance) - leadgeClimbXOffset1, Mathf.Floor(PhysicsManager.Instance.ledgePosBot.y) + leadgeClimbYOffset1);
                PhysicsManager.Instance.ledgePos2 = new Vector2(Mathf.Floor(PhysicsManager.Instance.ledgePosBot.x + PhysicsManager.Instance.ledgeDistance) - leadgeClimbXOffset2, Mathf.Floor(PhysicsManager.Instance.ledgePosBot.y) + leadgeClimbYOffset2);
            }
            else
            {
                PhysicsManager.Instance.ledgePos1 = new Vector2(Mathf.Ceil(PhysicsManager.Instance.ledgePosBot.x + PhysicsManager.Instance.ledgeDistance) - leadgeClimbXOffset1, Mathf.Floor(PhysicsManager.Instance.ledgePosBot.y) + leadgeClimbYOffset1);
                PhysicsManager.Instance.ledgePos2 = new Vector2(Mathf.Ceil(PhysicsManager.Instance.ledgePosBot.x + PhysicsManager.Instance.ledgeDistance) - leadgeClimbXOffset2, Mathf.Floor(PhysicsManager.Instance.ledgePosBot.y) + leadgeClimbYOffset2);
            }
            PlayerMove.Instance.isNotInTutorial = false;
            //Hacer que el player no puueda girarse
            
        }

        if (canClimbLeadge)
        {
            transform.position = PhysicsManager.Instance.ledgePos1;
        }
        
        
    }
    public void FinishClimbLedge()
    {
        canClimbLeadge = false;
        transform.position = PhysicsManager.Instance.ledgePos2;
        PlayerMove.Instance.isNotInTutorial = true;
        ledgeDetected = false;
    }
}
