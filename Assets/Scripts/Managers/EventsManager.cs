using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public static class EventsManager
{
        public static event UnityAction RoomClear;
    public static void OnRoomClear() => RoomClear?.Invoke();
    
}
