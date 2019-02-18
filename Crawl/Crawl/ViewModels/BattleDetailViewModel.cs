using Crawl.Models;

namespace Crawl.ViewModels
{
    public class BattleDetailViewModel : BaseViewModel
    {
        public Character DataC { get; set; }
        public Monster DataM { get; set; }

        public BattleDetailViewModel(Character dataC = null, Monster dataM = null)
        {
            Title = dataC?.Name;
            DataC = dataC;
            DataM = dataM;
        }
    }
}
