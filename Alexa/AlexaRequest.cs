using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenerifeDevAlexaSkill.Alexa
{

    public class AlexaRequest
    {
        public Session session { get; set; }
        public Request request { get; set; }
        public string version { get; set; }
    }

    public class Session
    {
        public string sessionId { get; set; }
        public Application application { get; set; }
        public CustomAttributes attributes { get; set; }
        public User user { get; set; }
        public bool _new { get; set; }
    }

    public class Application
    {
        public string applicationId { get; set; }
    }

    public class CustomAttributes
    {
        public string Language { get; set; }
    }

    public class User
    {
        public string userId { get; set; }
    }

    public class Request
    {
        public string type { get; set; }
        public string requestId { get; set; }
        public string locale { get; set; }
        public DateTime timestamp { get; set; }
        public Intent intent { get; set; }
    }

    public class Intent
    {
        public string name { get; set; }
        public dynamic slots { get; set; }

        private List<KeyValuePair<string, string>> GetSlots()
        {
            var output = new List<KeyValuePair<string, string>>();
            if (slots == null) return output;

            foreach (var slot in slots.Children())
            {
                if (slot.First.value != null)
                    output.Add(new KeyValuePair<string, string>(slot.First.name.ToString(), slot.First.value.ToString()));
            }

            return output;
        }

        public string GetSlotValue(string slotName)
        {
            var slots = GetSlots();
            if (slots.Any())
                return GetSlots().First(s => s.Key == slotName).Value;
            return null;

        }


    }

    public class Slots
    {
        public Uplimit UpLimit { get; set; }
        public Lowlimit LowLimit { get; set; }
    }

    public class Uplimit
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Lowlimit
    {
        public string name { get; set; }
        public string value { get; set; }
    }

}