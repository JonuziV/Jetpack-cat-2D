using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Tabtale.Services
{
    using EventData = Dictionary<string, object>;

    public static class LogService
    {
        public static void SendMissionStarted(EventData data)
        {
            SendEvent("missionStarted", data);
        }

        public static void SendMissionFailed(EventData data)
        {
            SendEvent("missionFailed", data);
        }

        public static void SendMissionCompleted(EventData data)
        {
            SendEvent("missionCompleted", data);
        }

        public static void SendEvent(string name, EventData data)
        {
            data = data ?? new EventData();

            var text = new StringBuilder();
            text.AppendFormat("Sending Event::{0}", name);

            foreach (var element in data)
                text.AppendFormat("\n\t{0}::{1}", element.Key, element.Value);

            Debug.Log(text.ToString());
        }
    }
}
