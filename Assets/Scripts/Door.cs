using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

  [SerializeField] List<Target> targets = new List<Target>();

  void Start()
  {

  }


  public void UpdateTargets(Target targetDestroy)
  {
    targets.Remove(targetDestroy);
    if (targets.Count <= 0)
    {
      Destroy(gameObject);
    }
  }
}
