using System.Collections.Generic;

namespace ViewModel.StartGame
{
    public class InnerGameViewModel
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int NumberOfBots { get; set; }
        public string DealerName { get; set; }
        public List<string>NameOfBots { get; set; } = new List<string>();
    }
}
