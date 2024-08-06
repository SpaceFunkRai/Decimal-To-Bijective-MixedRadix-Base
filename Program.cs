public class Program
{
    public static int mainRadix = 60;
    public static int subMinorRadix = 6;
    public static int subMajorRadix = 10;

    public static void Main()
    {
        string NM = Console.IsOutputRedirected ? "" : "\x1b[97m";
        string GR = Console.IsOutputRedirected ? "" : "\x1b[92m";
        string YE = Console.IsOutputRedirected ? "" : "\x1b[93m";

        Console.WriteLine("———Decimal To MixedRadixBijective———");
        Console.WriteLine($"{NM}Enter an {GR}integer{NM} (or type 'exit' to quit): ");
        
        while (true)
        {
            string? input = Console.ReadLine();

            if (input?.ToLower() == "exit")
                break;
            

            if (int.TryParse(input, out int number))
                Console.WriteLine($"Decimal {GR}{number}{NM} is {GR}{GetSubBase(ParseBase(ConvertToBase(number, mainRadix)))}{NM} in base {YE}{mainRadix}{NM} bijective with {YE}[{subMajorRadix}, {subMinorRadix}]{NM} sub-radices.");
            
            else
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            
        }
    }

    private static string GetSubBase(int[] subBases)
    {
        string sub = "";

        foreach (int subBase in subBases)
        {
            string s = "";

            for (int i = 0; i < ParseBase(ConvertToBase(subBase, subMinorRadix, subMajorRadix)).Length; i++)
            {
                if (i % 2 == 0)
                    s += ParseBase(ConvertToBase(subBase, subMinorRadix, subMajorRadix))[i];

                else if (i % 2 == 1)
                    s += Units(ParseBase(ConvertToBase(subBase, subMinorRadix, subMajorRadix))[i]);
            }

            bool b = int.TryParse(s, out int n);

            if (n <= 6 && b)
                s = Units(n);

            sub += s;
        }

        return sub;
    }

    static string Units(int number) => number switch
    {
        0 => "0",
        1 => "A",
        2 => "B",
        3 => "C",
        4 => "D",
        5 => "E",
        6 => "F",
        _ => "",
    };

    static string ConvertToBase (int dec, int radix, int subRadix = -1, bool bijective = true)
    {
        if (dec == 0)
            return "0";

        int carryRadix = subRadix == -1 ? radix : subRadix;
        
        int quotient  = dec / radix;
        int remainder = dec % radix;

        if (bijective && remainder == 0)
        {
            remainder = radix;
            quotient--;
        }

        if (quotient == 0)
            return $"{remainder}";
        
        else if (quotient > radix)
            return $"{ConvertToBase(quotient, carryRadix)}:{remainder}";
        
        else
            return $"{quotient}:{remainder}";
    }

    static int[] ParseBase(string s) => s.Split(':').Select(int.Parse).ToArray();
}
