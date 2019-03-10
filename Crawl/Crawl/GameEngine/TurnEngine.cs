using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Threading.Tasks;



using Crawl.Models;
using Crawl.ViewModels;
using System.Linq;
using Crawl.GameEngine;
using Crawl.Views.Battle;


namespace Crawl.GameEngine
{

    /// * 
    // * Need to decide who takes the next turn
    // * Target to Attack
    // * Should Move, or Stay put (can hit with weapon range?)
    // * Death
    // * Manage Round...
    // * /

    public class TurnEngine
    {


        // Holds the official score
        public Score BattleScore = new Score();

        // battle messages
        public BattleMessages BattleMessages = new BattleMessages();

        public string AttackerName = string.Empty;
        public string TargetName = string.Empty;
        public string AttackStatus = string.Empty;

        public string TurnMessage = string.Empty;
        public string TurnMessageSpecial = string.Empty;
        public string LevelUpMessage = string.Empty;

        public int DamageAmount = 0;
        public HitStatusEnum HitStatus = HitStatusEnum.Unknown;

        public List<Item> ItemPool = new List<Item>();

        //public List<Item> ItemList = new List<Item>();

        public List<Monster> MonsterList = new List<Monster>();
        public List<Character> CharacterList = new List<Character>();
        

        // Attack or Move
        // Roll To Hit
        // Decide Hit or Miss
        // Decide Damage
        // Death
        // Drop Items
        // Turn Over

        // Character Attacks...
        public bool TakeTurn(Character Attacker)
        {
            //use passed in character to attack from the round engine
          
            // For Attack, choose the first monster in list to attack
            var Target = AttackChoice(Attacker);

            // Do Attack
            //characters attack level and attack amount based on level and bonus from items
            var AttackScore = Attacker.Level + Attacker.GetAttack();
            //monsters defence score
            var DefenseScore = Target.GetDefense() + Target.Level;
            //attack the monster or  miss
            TurnAsAttack(Attacker, AttackScore, Target, DefenseScore);
            return true;
        }

        // Monster Attacks...
        public bool TakeTurn(Monster Attacker)
        {
            // For Attack choose the first character in list to attack
            var Target = AttackChoice(Attacker);

            // Do Attack
            //monster attack level and attack amount based on level and bonus from items
            var AttackScore = Attacker.Level + Attacker.GetAttack();
            //character defence score
            var DefenseScore = Target.GetDefense() + Target.Level;
            //attack the character or miss
            TurnAsAttack(Attacker, AttackScore, Target, DefenseScore);
            return true;
        }

        // Monster Attacks Character (monsters don't get xp earned and their level is scaled based on instantiation)
        public bool TurnAsAttack(Monster Attacker, int AttackScore, Character Target, int DefenseScore)
        {
            TurnMessage = string.Empty;
            TurnMessageSpecial = string.Empty;
            AttackStatus = string.Empty;

            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            BattleScore.TurnCount++;

            //Names for attacker and target
            TargetName = Target.Name;
            AttackerName = Attacker.Name;

            var HitSuccess = RollToHitTarget(AttackScore, DefenseScore);

            // Miss
            if (HitStatus == HitStatusEnum.Miss)
            {
                TurnMessage = Attacker.Name + " misses " + Target.Name;
                Debug.WriteLine(TurnMessage);
                return true;
            }

            //Force a miss if it's a critical miss rolls a 1 or 2
            if (HitStatus == HitStatusEnum.CriticalMiss)
            {
                Debug.WriteLine("#########################");
                Debug.WriteLine("Great Luck, Monster rolled 1 or 2 so the monster critically MISSED!!!");
                TurnMessage = Attacker.Name + " bad roll of 1 or 2 so the monster  critically misses " + Target.Name;
                Debug.WriteLine(TurnMessage);
                return true;
            }

            // It's a Hit or a Critical Hit (rolls 20 or 19)
            //Calculate Damage
            DamageAmount = Attacker.GetDamageRollValue();

            DamageAmount += GameGlobals.ForceMonsterDamangeBonusValue;  // Add The forced damage bonus

            if (HitStatus == HitStatusEnum.Hit)
            {
                Target.TakeDamage(DamageAmount);
                AttackStatus = string.Format(" hits for {0} damage on ", DamageAmount);
            }

            // double damage
            if (HitStatus == HitStatusEnum.CriticalHit)
            {
                //2x damage
                DamageAmount += DamageAmount;
                Debug.WriteLine("#########################");
                Debug.WriteLine("Unfortunately Monster rolled a 19 or 20 so it critically HIT your character!!!");
                Target.TakeDamage(DamageAmount);
                AttackStatus = string.Format(" hits critically hard for {0} damage on ", DamageAmount);
            }

            //Display if character is still alive
            if (Target.Attribute.CurrentHealth > 0)
            {
                TurnMessageSpecial = " remaining health is " + Target.Attribute.CurrentHealth;
            }

            // Check for alive
            if (Target.Alive == false)
            {
                // Remover target from list...
                CharacterList.Remove(Target);

                // Mark Status in output
                TurnMessageSpecial = " and causes death";

                // Add the monster to the killed list
                BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                // Drop Items to item Pool
                var myItemList = Target.DropAllItems();

                // Add to Score
                foreach (var item in myItemList)
                {
                    BattleScore.ItemsDroppedList += item.FormatOutput() + "\n";
                    TurnMessageSpecial += " Item " + item.Name + " dropped";
                }

                ItemPool.AddRange(myItemList);
            }

            TurnMessage = Attacker.Name + AttackStatus + Target.Name + TurnMessageSpecial;
            Debug.WriteLine(TurnMessage);
            return true;
        }

        // Character attacks Monster
        public bool TurnAsAttack(Character Attacker, int AttackScore, Monster Target, int DefenseScore)
        {


            TurnMessage = string.Empty;
            TurnMessageSpecial = string.Empty;
            AttackStatus = string.Empty;
            LevelUpMessage = string.Empty;

            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            BattleScore.TurnCount++;

            //Names for attacker and target
            TargetName = Target.Name;
            AttackerName = Attacker.Name;

            // Determine whether hit or miss based on character attack total and monster defense total
            var HitSuccess = RollToHitTarget(AttackScore, DefenseScore);


            Debug.WriteLine("#############");
      
         
            //miss based on attack and roll being less than defence
            if (HitStatus == HitStatusEnum.Miss)
            {
                TurnMessage = Attacker.Name + " misses " + Target.Name;
                Debug.WriteLine(TurnMessage);

                return true;
            }

            // force a miss if you roll a 1 or 2 no matter what the attack is
            if (HitStatus == HitStatusEnum.CriticalMiss)
            {
                Debug.WriteLine("#########################");
                Debug.WriteLine("Unfortunately Character rolled 1 or 2 so you critically MISSED!!!");
                TurnMessage = Attacker.Name + " rolls 1 or 2 and the character critically misses " + Target.Name;
                Debug.WriteLine(TurnMessage);

                return true;
            }

            // It's a Hit if your attack and roll is greater than the defence
            if (HitStatus == HitStatusEnum.Hit || HitStatus == HitStatusEnum.CriticalHit)
            {
                //Calculate Damage
                DamageAmount = Attacker.GetDamageRollValue();

                DamageAmount += GameGlobals.ForceCharacterDamangeBonusValue;   // Add the Forced Damage Bonus (used for testing...)

                AttackStatus = string.Format(" hits for {0} damage on ", DamageAmount);


                // double the damage if critical hit and character rolls 20 or 19
                if (GameGlobals.EnableCriticalHitDamage)
                {
                    if (HitStatus == HitStatusEnum.CriticalHit)
                    {
                        Debug.WriteLine("#########################");
                        Debug.WriteLine("Amazing Roll, Character rolled 19 or 20 so critically HIT!!!");
                        //2x damage
                        DamageAmount += DamageAmount;
                        AttackStatus = string.Format(" hits critically hard for {0} damage on ", DamageAmount);
                    }
                }

                //the monster takes the damage
                Target.TakeDamage(DamageAmount);

                // calculate experience earned
                var experienceEarned = Target.CalculateExperienceEarned(DamageAmount);

                
                // check whether enough xp is given to level up
                var LevelUp = Attacker.AddExperience(experienceEarned);
                if (LevelUp)
                {
                    LevelUpMessage = Attacker.Name + " is now Level " + Attacker.Level + " With Health Max of " + Attacker.GetHealthMax();
                    Debug.WriteLine(LevelUpMessage);
                }

                BattleScore.ExperienceGainedTotal += experienceEarned;
            }

            //display health of Monster if still alive
            if (Target.Attribute.CurrentHealth > 0)
            {
                TurnMessageSpecial = " remaining health is " + Target.Attribute.CurrentHealth;
            }

            // Check for alive
            if (Target.Alive == false)
            {
                // Remover target from list...
                MonsterList.Remove(Target);

                // Mark Status in output
                TurnMessageSpecial = " and causes death";

                // Add one to the monsters killd count...
                BattleScore.MonsterSlainNumber++;

                // Add the monster to the killed list
                BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                // Drop Items to item Pool
                var myItemList = Target.DropAllItems();

                // If Random drops are enabled, then add some....
                myItemList.AddRange(GetRandomMonsterItemDrops(BattleScore.RoundCount));

                // Add to Score
                foreach (var item in myItemList)
                {
                    BattleScore.ItemsDroppedList += item.FormatOutput() + "\n";
                    TurnMessageSpecial += " Item " + item.Name + " dropped";
                }

                ItemPool.AddRange(myItemList);
            }

            TurnMessage = Attacker.Name + AttackStatus + Target.Name + TurnMessageSpecial;
            Debug.WriteLine(TurnMessage);
            return true;
        }

        public HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {

            var d20 = HelperEngine.RollDice(1, 20);

            // Turn On UnitTestingSetRoll
            if (GameGlobals.ForceRollsToNotRandom)
            {
                // Don't let it be 0, if it was not initialized...
                if (GameGlobals.ForceToHitValue < 1)
                {
                    GameGlobals.ForceToHitValue = 1;
                }

                // result of the dice roll between 1 to 20
                d20 = GameGlobals.ForceToHitValue;
            }

            //critical miss then force a miss if 1 or 2 is rolled
            if (d20 == 1 || d20 == 2)
            {
                // Force Miss
                HitStatus = HitStatusEnum.CriticalMiss;
                return HitStatus;
            }

            //critical hit if 20 or 19 is rolled then force a hit
            if (d20 == 20 || d20 ==19)
            {
                // Force Hit
                HitStatus = HitStatusEnum.CriticalHit;
                return HitStatus;
            }

            //total hitscore is based on attack and the dice.
            var ToHitScore = d20 + AttackScore;

            // if the attack with the dice roll is less than defence score then its a miss
            if (ToHitScore < DefenseScore)
            {
                AttackStatus = " misses ";
                // Miss
                HitStatus = HitStatusEnum.Miss;
                DamageAmount = 0;
            }
            else // if attack and dice is more than defence then its a hit
            {
                // Hit
                HitStatus = HitStatusEnum.Hit;
            }

            return HitStatus;
        }

        //For now choose first monster in list to get attacked
        public Monster AttackChoice(Character data)
        {
            if (MonsterList == null)
            {
                return null;
            }

            if (MonsterList.Count < 1)
            {
                return null;
            }

            // For now, just use a simple selection of the first in the list.
            // Later consider, strongest, closest, with most Health etc...
            foreach (var Defender in MonsterList)
            {
                if (Defender.Alive)
                {
                    // Select first one to hit in the list for now...
                    return Defender;
                }
            }
            return null;
        }

        //For now have first character in list to be the one who gets attacked
        public Character AttackChoice(Monster data)
        {
            //needs to have a character to return
            if (CharacterList == null)
            {
                return null;
            }

            if (CharacterList.Count < 1)
            {
                return null;
            }

            // For now, just use a simple selection of the first in the list.
            // Later consider, strongest, closest, with most Health etc...
            foreach (var Defender in CharacterList)
            {
                if (Defender.Alive)
                {
                    // Select first one to hit in the list for now...
                    return Defender;
                }
            }
            return null;
        }

        // Will drop between 1 and 4 items from the item set...
        public List<Item> GetRandomMonsterItemDrops(int round)
        {
            var myList = new List<Item>();

            if (!GameGlobals.AllowMonsterDropItems)
            {
                return myList;
            }

            var myItemsViewModel = ItemsViewModel.Instance;

            if (myItemsViewModel.Dataset.Count > 0)
            {
                // Random is enabled so build up a list of items dropped...
                var ItemCount = HelperEngine.RollDice(1, 4);
                for (var i = 0; i < ItemCount; i++)
                {
                    var rnd = HelperEngine.RollDice(1, myItemsViewModel.Dataset.Count);
                    var itemBase = myItemsViewModel.Dataset[rnd - 1];
                    var item = new Item(itemBase);
                    item.ScaleLevel(round);

                    // Make sure the item is added to the global list...
                    var myItem = ItemsViewModel.Instance.CheckIfItemExists(item);
                    if (myItem == null)
                    {
                        // Item does not exist, so add it to the datstore

                        // TODO:  Need way to not save the Item
                        ItemsViewModel.Instance.InsertUpdateAsync(item).GetAwaiter().GetResult();
                    }
                    else
                    {
                        // Swap them becaues it already exists, no need to create a new one...
                        item = myItem;
                    }

                    // Add the item to the local list...
                    myList.Add(item);
                }
            }

            return myList;
        }

        
   
    }
}
