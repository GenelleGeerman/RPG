using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    [SerializeField] private Status currentStatus;
    public Status CurrentStatus { get => currentStatus; set => currentStatus = value; }

    private IStatus none;
    private IStatus bleed;
    private IStatus slow;
    private IStatus defense;
    private IStatus seeking;
    private Dictionary<Status, IStatus> statuses = new();

    private void Start()
    {
        InitializeStatuses();
    }

    private void InitializeStatuses()
    {
        none = gameObject.AddComponent<NoneStatus>();
        bleed = gameObject.AddComponent<BleedStatus>();
        slow = gameObject.AddComponent<SlowStatus>();
        defense = gameObject.AddComponent<DefenseStatus>();
        seeking = gameObject.AddComponent<SeekingStatus>();
        statuses.Add(Status.None, none);
        statuses.Add(Status.Bleed, bleed);
        statuses.Add(Status.Slow, slow);
        statuses.Add(Status.Defense, defense);
        statuses.Add(Status.Seeking, seeking);
    }

    private void SetStatus(Status status)
    {
        currentStatus = status;
    }
    public IStatus GetStatusEffect()
    {
        return statuses[currentStatus];
    }

    public void AddStatusEffect(Status status, IStatus effect)
    {
        statuses.Add(status, effect);
    }

}
