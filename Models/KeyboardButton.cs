using System.ComponentModel.DataAnnotations;

namespace BankPassword.Models {
    public class KeyboardButton {
        [Range(0, 9)]
        public byte FirstNumber { get; set; }
        [Range(0, 9)]
        public byte SecondNumber { get; set; }
    }
}
