using System;
using UnityEngine;

namespace Project.Modules.Network
{
    public class LeaveRoomButton : MonoBehaviour
    {
        public static Action LeaveRoomAction;

        public void CancelJoinRoom()
        {
            Stepsss.LeaveRoomRequest();
            NetworkData.CloseNetwork();
            LeaveRoomAction?.Invoke();
        }
    }
}
