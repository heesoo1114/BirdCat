using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents;
using UnityEngine;

public class DoughnutAgent : Agent
{
    [SerializeField] private bool isLearning = false;

    [SerializeField] private float strength = 5f;
    private float gravity = -9.81f;
    private Vector3 direction;

    private LineController lineController;

    private Vector3 initPosition;

    public override void Initialize()
    {
        lineController = transform.root.Find("LineObject").GetComponent<LineController>();

        initPosition = transform.localPosition;

        InitMovement();
    }

    private void Update()
    {
        if (isLearning) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // 플레이어에 중력 적용
        direction.y += gravity * Time.fixedDeltaTime;
        direction.x = 1f;
        transform.localPosition += direction * Time.fixedDeltaTime;
    }

    private void Jump()
    {
        direction = Vector3.up * strength;
        // lineController.AddPoint(); // for easy play
    }

    private void InitMovement()
    {
        transform.localPosition = initPosition;
        direction = Vector3.zero;
    }

    public override void OnEpisodeBegin()
    {
        // lineController.Reset();
        InitMovement();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var discreteActions = actions.DiscreteActions;

        if (discreteActions[0] == 1)
        {
            Jump();
        }

        AddReward(0.01f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreateActionsOut = actionsOut.DiscreteActions;
    
        if (Input.GetKey(KeyCode.Space))
        {
            discreateActionsOut[0] = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Line"))
        {
            if (isLearning)
            {
                SetReward(-10f);
                EndEpisode();
            }
            else
            {
                InitMovement();
                lineController.Reset();
            }
        }
    }

}
