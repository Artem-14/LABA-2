using System;

namespace FractionApp
{
    // Пользовательское исключение
    public class NotCorrectlyDenominatorException : ArgumentException
    {
        public NotCorrectlyDenominatorException() : base("Знаменатель не может быть равен 0") { }
    }

    public class Fraction
    {
        private int numerator;
        private int denominator;

        public int Numerator
        {
            get => numerator;
            set
            {
                numerator = value;
                Simplify();
            }
        }

        public int Denominator
        {
            get => denominator;
            set
            {
                if (value == 0)
                    throw new NotCorrectlyDenominatorException();
                denominator = value;
                Simplify();
            }
        }

        // Только для чтения
        public double DecimalValue => (double)numerator / denominator;

        // Конструктор
        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new NotCorrectlyDenominatorException();
            this.numerator = numerator;
            this.denominator = denominator;
            Simplify();
        }

        // Сложение
        public Fraction Add(Fraction other)
        {
            int newNumerator = numerator * other.denominator + other.numerator * denominator;
            int newDenominator = denominator * other.denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        // Вычитание
        public Fraction Subtract(Fraction other)
        {
            int newNumerator = numerator * other.denominator - other.numerator * denominator;
            int newDenominator = denominator * other.denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        // Умножение
        public Fraction Multiply(Fraction other)
        {
            return new Fraction(numerator * other.numerator, denominator * other.denominator);
        }

        // Деление
        public Fraction Divide(Fraction other)
        {
            if (other.numerator == 0)
                throw new DivideByZeroException("Деление на дробь с нулевым числителем");
            return new Fraction(numerator * other.denominator, denominator * other.numerator);
        }

        // Упрощение дроби
        private void Simplify()
        {
            int gcd = GCD(Math.Abs(numerator), Math.Abs(denominator));
            numerator /= gcd;
            denominator /= gcd;
            // Делать знаменатель всегда положительным
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }
        }

        // Наибольший общий делитель
        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int tmp = b;
                b = a % b;
                a = tmp;
            }
            return a;
        }

        public override string ToString()
        {
            return $"{numerator}/{denominator}";
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {
                var f1 = new Fraction(3, 4);
                var f2 = new Fraction(5, 6);

                Console.WriteLine($"f1: {f1} = {f1.DecimalValue}");
                Console.WriteLine($"f2: {f2} = {f2.DecimalValue}");

                Console.WriteLine($"Сумма: {f1.Add(f2)}");
                Console.WriteLine($"Разность: {f1.Subtract(f2)}");
                Console.WriteLine($"Произведение: {f1.Multiply(f2)}");
                Console.WriteLine($"Частное: {f1.Divide(f2)}");

                // Проверка исключения
                // var f3 = new Fraction(1, 0); // Раскомментируй, чтобы проверить
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
