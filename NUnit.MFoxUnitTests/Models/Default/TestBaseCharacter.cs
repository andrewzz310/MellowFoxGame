﻿using Crawl.Models;
using Newtonsoft.Json.Linq;

namespace NUnit.MFoxUnitTests.Models.Default
{
    public static partial class DefaultModels
    {

        public static BaseCharacter BaseCharacterDefault()
        {
            var myData = new BaseCharacter();
            
            myData.Alive = true;

            // Base information
            myData.Name = "Name";
            myData.Description = "Description";
            myData.Level = 1;
            myData.ExperienceTotal = 0;
            myData.ImageURI = null;

            var myAttributes = new AttributeBase();
            myAttributes.Attack = 1;
            myAttributes.Speed = 1;
            myAttributes.MaxHealth = 1;
            myAttributes.CurrentHealth = 1;
            myAttributes.Defense = 1;

            JObject myAttributesJson = (JObject)JToken.FromObject(myAttributes);
            var myAttibutesString = myAttributesJson.ToString();

            myData.AttributeString = myAttibutesString;

            return myData;
        }

    }
}
