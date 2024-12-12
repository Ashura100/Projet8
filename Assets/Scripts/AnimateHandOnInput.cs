using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class AnimationInput
{
    public string animationPropertyName;
    public InputActionProperty actions;

}
public class AnimateHandOnInput : MonoBehaviour
{
    public List<AnimationInput> input;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in input)
        {
            float actionValue = item.actions.action.ReadValue<float>();
            animator.SetFloat(item.animationPropertyName, actionValue);
        }
    }
}
