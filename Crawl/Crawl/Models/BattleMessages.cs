using System;
using System.Collections.Generic;
using System.Text;
using Crawl.Models.Enums;
using Crawl.Models;

namespace Crawl.Models
{
    public class BattleMessages
    {
        public PlayerTypeEnum PlayerType;

        public string AttackerName = string.Empty;
        public string TargetName = string.Empty;
        public string AttackStatus = string.Empty;

        public string TurnMessage = string.Empty;
        public string TurnMessageSpecial = string.Empty;
        public string LevelUpMessage = string.Empty;

        public int DamageAmount = 0;
        public int CurrentHealth = 0;

        public HitStatusEnum HitStatus = HitStatusEnum.Unknown;

        /// <summary>
        /// Return formatted string
        /// </summary>
        /// <param name="hitStatus"></param>
        /// <returns></returns>
        public string GetSwingResult()
        {
            return BattleMessageHitEnum.Instance.GetMessage(HitStatus);
        }

        /// <summary>
        /// Return formatted Damage
        /// </summary>
        /// <returns></returns>
        public string GetDamageMessage()
        {
            return string.Format(" for {0} damage ", DamageAmount);
        }

        /// <summary>
        /// Returns the String Attacker Hit Defender
        /// </summary>
        /// <returns></returns>
        public string GetTurnMessage()
        {
            return AttackerName + GetSwingResult() + TargetName;
        }

        /// <summary>
        /// Remaining Health Message
        /// </summary>
        /// <returns></returns>
        public string GetCurrentHealthMessage()
        {
            return " remaining health is " + CurrentHealth.ToString();
        }

        public string GetHTMLFormattedTurnMessage()
        {
            var myResult = string.Empty;

            var htmlHead = @"<html><body bgcolor=""#F0FFFF""><p>";
            var htmlTail = @"</p></body></html>";


            //AttackerName 
            //GetSwingResult ();
            // TargetName
            // BattleMessages.TurnMessageSpecial;

            //style = "color:Tomato;"

            var AttackerStyle = @"<span style=""color:blue"">";
            var DefenderStyle = @"<span style=""color:green"">";
            if (PlayerType == PlayerTypeEnum.Monster)
            {
                // If monster, swap the colors
                DefenderStyle = @"<span style=""color:blue"">";
                AttackerStyle = @"<span style=""color:green"">";
            }

            var SwingResult = string.Empty;
            switch (HitStatus)
            {
                case HitStatusEnum.Miss:
                    SwingResult = @"<span style=""color:yellow"">";
                    break;

                case HitStatusEnum.CriticalMiss:
                    SwingResult = @"<span bold style=""color:yellow; font-weight:bold;"">";
                    break;

                case HitStatusEnum.CriticalHit:
                    SwingResult = @"<span bold style=""color:red; font-weight:bold;"">";
                    break;

                case HitStatusEnum.Hit:
                default:
                    SwingResult = @"<span style=""color:red"">";
                    break;
            }

            var htmlBody = string.Empty;
            htmlBody += string.Format(@"{0}{1}</span>", AttackerStyle, AttackerName);
            htmlBody += string.Format(@"{0}{1}</span>", SwingResult, GetSwingResult());
            htmlBody += string.Format(@"{0}{1}</span>", DefenderStyle, TargetName);
            htmlBody += string.Format(@"<span>{0}</span>", TurnMessageSpecial);

            myResult = htmlHead + htmlBody + htmlTail;
            return myResult;
        }
    }
}
