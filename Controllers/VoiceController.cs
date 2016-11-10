﻿using System;
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
            return new AlexaResponse("This is tenerife dev skill developed in asp.net web api. Ask for a random number or tell me what is your favorite language. What do you want to do?", false);


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
                    return GetRandomNumberHandler(alexaRequest);
                default:
                    return LaunchRequestHandler(alexaRequest);

            }
        }

        private AlexaResponse GetRandomNumberHandler(AlexaRequest alexaRequest)
        {
            throw new NotImplementedException();
        }

        private AlexaResponse WhatsMyLanguageHandler(AlexaRequest alexaRequest)
        {
            var lang = alexaRequest.session?.attributes?.Language;
            if (string.IsNullOrEmpty(lang))
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
