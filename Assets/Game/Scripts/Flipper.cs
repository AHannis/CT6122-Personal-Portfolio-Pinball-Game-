using UnityEngine;
using UnityEngine.InputSystem;

public class Flipper : MonoBehaviour
{
    [SerializeField] float hitStrength = 80000f;
    [SerializeField] float dampening = 250f;

    [SerializeField] HingeJoint hingeJointLeft;
    [SerializeField] HingeJoint hingeJointRight;

    private bool leftFlipperPressed;
    private bool rightFlipperPressed;

    JointSpring CreateSpring(float targetPosition)
    {
        JointSpring spring = new JointSpring
        {
            spring = hitStrength,
            damper = dampening,
            targetPosition = targetPosition
        };
        return spring;
    }

    void Awake()
    {
        if (hingeJointLeft == null)
        {
            Debug.LogError("Left HingeJoint not assigned on Flipper.", this);
            enabled = false;
            return;
        }

        if (hingeJointRight == null)
        {
            Debug.LogError("Right HingeJoint not assigned on Flipper.", this);
            enabled = false;
            return;
        }

        // Force both flippers DOWN before physics runs
        hingeJointLeft.spring = CreateSpring(hingeJointLeft.limits.min);
        hingeJointRight.spring = CreateSpring(hingeJointRight.limits.min);
    }

    private void OnLeftFlipper(InputValue value)
    {
        leftFlipperPressed = value.isPressed;
    }

    private void OnRightFlipper(InputValue value)
    {
        rightFlipperPressed = value.isPressed;
    }

    void Update()
    {
        if (hingeJointLeft == null || hingeJointRight == null)
            return;

        // LEFT FLIPPER
        hingeJointLeft.spring = leftFlipperPressed
            ? CreateSpring(hingeJointLeft.limits.max)
            : CreateSpring(hingeJointLeft.limits.min);

        // RIGHT FLIPPER
        hingeJointRight.spring = rightFlipperPressed
            ? CreateSpring(hingeJointRight.limits.max)
            : CreateSpring(hingeJointRight.limits.min);
    }
}
