using System;
using System.Collections.Generic;
using System.Text;

namespace Crawl.Models
{
    public class NewCharacter
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Level { get; set; }

        public int ExperienceTotal { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public string ImageURI { get; set; }

        //Preferred Item cause 2x damage
        public PreferredItemEnum Item { get; set; }


        public void Update(Character newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id
            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
        }

    }
}
 
