using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenerifeDevAlexaSkill.Alexa
{

    public class AlexaResponse
    {
        public string version { get; set; }
        public Response response { get; set; }
        public CustomAttributes sessionAttributes { get; set; }

        public AlexaResponse()
        {
            version = "1.0";
            sessionAttributes = new CustomAttributes();
            response = new Response()
            {
                outputSpeech = new Outputspeech(),
                card = new Card(),
                reprompt = new Reprompt() { outputSpeech = new Outputspeech() }

            };

        }

        public AlexaResponse(string outputSpeechText) : this()
        {
            response.outputSpeech.text = outputSpeechText;
            response.card.content = outputSpeechText;
        }

        public AlexaResponse(string outputSpeechText, bool shouldEndSession) : this()
        {
            response.outputSpeech.text = outputSpeechText;


            if (shouldEndSession)
            {
                response.card.content = outputSpeechText;
            }
            else
            {
                response.card = null;
            }
        }

        public AlexaResponse(string outputSpeechText, string cardContent)
           : this()
        {
            response.outputSpeech.text = outputSpeechText;
            response.card.content = cardContent;
        }

    }



    public class Response
    {
        public Outputspeech outputSpeech { get; set; }
        public Card card { get; set; }
        public Reprompt reprompt { get; set; }
        public bool shouldEndSession { get; set; }
    }

    public class Outputspeech
    {
        public string type { get; set; }
        public string text { get; set; }

        public Outputspeech()
        {
            type = "PlainText";
        }
    }



    public class Card
    {
        public string content { get; set; }
        public string title { get; set; }
        public string type { get; set; }
    }

    public class Reprompt
    {
        public Outputspeech outputSpeech { get; set; }
    }


   

}