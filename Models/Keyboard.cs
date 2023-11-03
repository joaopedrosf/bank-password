namespace BankPassword.Models {
    public class Keyboard {
        public KeyboardButton[] Buttons { get; set; } = new KeyboardButton[5];

        public Keyboard() {
            Generate();
        }

        public void Generate() {
            var numberList = new List<byte>(10) { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var random = new Random();
            for (int i = 0; i < Buttons.Length; i++) {
                var randomIndex = random.Next(0, numberList.Count);
                var firstNumber = numberList[randomIndex];
                numberList.RemoveAt(randomIndex);

                randomIndex = random.Next(0, numberList.Count);
                var secondNumber = numberList[randomIndex];
                numberList.RemoveAt(randomIndex);

                Buttons[i] = new KeyboardButton(firstNumber, secondNumber);
            }
        }
    }
}
