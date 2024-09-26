using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _viewModel;
    public void PlayAnim(int hash, int layer = -1, float normalizedTime = 0)
    {
        _viewModel.Play(hash, layer, normalizedTime);
    }
}
