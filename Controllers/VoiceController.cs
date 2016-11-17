using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TenerifeDevAlexaSkill.Alexa;

namespace TenerifeDevAlexaSkill.Controllers
{
    public class VoiceController : ApiController
    {

        private static int lastNumber = -1;
        public AlexaResponse TenerifeDev(AlexaRequest alexaRequest)
        {
            switch (alexaRequest.request.type)
            {
                case "LaunchRequest":
                    return LaunchRequestHandler(alexaRequest);
                case "IntentRequest":
                    return IntentRequestHandler(alexaRequest);
                case "SessionEndedRequest":
                    return SessionEndedRequestHandler(alexaRequest);
                default:
                    return LaunchRequestHandler(alexaRequest);

            }
        }


        private AlexaResponse LaunchRequestHandler(AlexaRequest alexaRequest)
        {
            return new AlexaResponse("This is tenerife dev skill developed in ASP dot net web API. Ask for a random number or tell me what is your favorite language. What do you want to do?", false);


        }

        private AlexaResponse IntentRequestHandler(AlexaRequest alexaRequest)
        {
            switch (alexaRequest.request.intent.name)
            {
                case "MyLanguageIsIntent":
                    return MyLanguageIsHandler(alexaRequest);
                case "WhatsMyLanguageIntent":
                    return WhatsMyLanguageHandler(alexaRequest);
                case "GetRandomNumberIntent":
                    return GetRandomNumberIntent(alexaRequest);
                case "GetLastNumberIntent":
                    return GetLastNumberHandler(alexaRequest);
                case "AMAZON.StopIntent":
                case "AMAZON.CancelIntent":
                    return new AlexaResponse("", true);
                    
                default:
                    return LaunchRequestHandler(alexaRequest);

            }
        }

        private AlexaResponse SessionEndHandler(AlexaRequest alexaRequest)
        {
            return new AlexaResponse("Thank you for your attention. I hope you\'ve learned something today and perhaps will even try to write Alexa skill your self. Have a nice evening!",true);
            
        }

        private AlexaResponse GetLastNumberHandler(AlexaRequest alexaRequest)
        {
            if (lastNumber == -1)
                return new AlexaResponse($"Sorry, I can't remember the last random number. Please ask for a new random number by saying, what is a random number between one and sixty.", false);
            else
                return new AlexaResponse($"The last random number was {lastNumber}.", true);

        }

        private AlexaResponse GetRandomNumberIntent(AlexaRequest alexaRequest)
        {
            var lowLimit = alexaRequest.request.intent.GetSlotValue("LowLimit");
            var upLimit = alexaRequest.request.intent.GetSlotValue("UpLimit");

            if (lowLimit == null || upLimit == null)
                return new AlexaResponse($"Sorry, you did not specify the low and up limits for the random number. Please try again.", false);

            int lLimit = int.Parse(lowLimit);
            int uLimit = int.Parse(upLimit);
            lastNumber = new Random().Next(lLimit, uLimit + 1);

            var speech = $"The random number between {lLimit} and {uLimit} is {lastNumber}.";
            return new AlexaResponse(speech, true);

        }

        private AlexaResponse WhatsMyLanguageHandler(AlexaRequest alexaRequest)
        {
            var lang = alexaRequest.session?.attributes?.Language;
            if (!string.IsNullOrEmpty(lang))
                return new AlexaResponse($"Your favorite language is {lang}. Goodbye ", true);
            return new AlexaResponse($"I am not sure what your favorite language is. You can tell me by saying, my favorite language is followed by the language you like the most.", false);
        }

        private AlexaResponse MyLanguageIsHandler(AlexaRequest alexaRequest)
        {
            var lang = alexaRequest.request.intent.GetSlotValue("Language");
            var rsp = new AlexaResponse($"I now know that your favorite language is {lang}. You can ask what is my favorite language. ", false);
            rsp.sessionAttributes.Language = lang;
            return rsp;
        }

        private AlexaResponse SessionEndedRequestHandler(AlexaRequest alexaRequest)
        {
            //cleanup if needed
            return null;
        }
    }
}
