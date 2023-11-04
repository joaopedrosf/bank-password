using BankPassword.Models;

namespace BankPassword.Services {
    public static class KeyboardPossibilitiesCalculator {
        public static List<string> CalculatePasswordPossibilities(Keyboard keyboard, List<byte> buttonSequence) {
            var buttonsClicked = new List<KeyboardButton>();
            foreach (var button in buttonSequence) {
                buttonsClicked.Add(keyboard.Buttons[button]);
            }
            var passwordPossibilities = new List<string>();
            CalculatePossibility(buttonsClicked, 0, string.Empty, passwordPossibilities);

            return passwordPossibilities;
        }

        private static void CalculatePossibility(List<KeyboardButton> buttonsClicked, int index, string currentCombination, List<string> passwordPossibilities) {
            if (index == buttonsClicked.Count) {
                passwordPossibilities.Add(currentCombination);
                return;
            }

            var button = buttonsClicked[index];

            CalculatePossibility(buttonsClicked, index + 1, currentCombination + button.FirstNumber, passwordPossibilities);
            CalculatePossibility(buttonsClicked, index + 1, currentCombination + button.SecondNumber, passwordPossibilities);
        }
    }
}
