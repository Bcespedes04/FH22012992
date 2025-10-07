using System.Text;

namespace BinarioApp.Models
{
    public static class BinaryCalculator
    {
        private static string TrimLeadingZeros(string s)
        {
            var t = s.TrimStart('0');
            return string.IsNullOrEmpty(t) ? "0" : t;
        }

        private static string AndString(string a, string b)
        {
            var max = Math.Max(a.Length, b.Length);
            a = a.PadLeft(max, '0');
            b = b.PadLeft(max, '0');

            var sb = new StringBuilder(max);
            for (int i = 0; i < max; i++)
                sb.Append(a[i] == '1' && b[i] == '1' ? '1' : '0');

            return TrimLeadingZeros(sb.ToString());
        }

        private static string OrString(string a, string b)
        {
            var max = Math.Max(a.Length, b.Length);
            a = a.PadLeft(max, '0');
            b = b.PadLeft(max, '0');

            var sb = new StringBuilder(max);
            for (int i = 0; i < max; i++)
                sb.Append(a[i] == '1' || b[i] == '1' ? '1' : '0');

            return TrimLeadingZeros(sb.ToString());
        }

        private static string XorString(string a, string b)
        {
            var max = Math.Max(a.Length, b.Length);
            a = a.PadLeft(max, '0');
            b = b.PadLeft(max, '0');

            var sb = new StringBuilder(max);
            for (int i = 0; i < max; i++)
                sb.Append(a[i] != b[i] ? '1' : '0');

            return TrimLeadingZeros(sb.ToString());
        }

        private static string ToBase(int value, int toBase)
            => Convert.ToString(value, toBase).ToUpperInvariant();

        public static List<OperationResult> ComputeAll(string a, string b)
        {
            // A y B en decimal (0..255)
            int aDec = Convert.ToInt32(a, 2);
            int bDec = Convert.ToInt32(b, 2);

            // Filas de A y B (Bin = 8 bits)
            var results = new List<OperationResult>
            {
                new OperationResult {
                    Item = "a",
                    Bin = a.PadLeft(8,'0'),
                    Oct = ToBase(aDec, 8),
                    Dec = aDec.ToString(),
                    Hex = ToBase(aDec, 16)
                },
                new OperationResult {
                    Item = "b",
                    Bin = b.PadLeft(8,'0'),
                    Oct = ToBase(bDec, 8),
                    Dec = bDec.ToString(),
                    Hex = ToBase(bDec, 16)
                }
            };

            // Operaciones binarias por strings
            var andBin = AndString(a, b);
            var orBin  = OrString(a, b);
            var xorBin = XorString(a, b);

            int andDec = Convert.ToInt32(andBin, 2);
            int orDec  = Convert.ToInt32(orBin, 2);
            int xorDec = Convert.ToInt32(xorBin, 2);

            results.Add(new OperationResult {
                Item = "a AND b",
                Bin  = andBin,
                Oct  = ToBase(andDec, 8),
                Dec  = andDec.ToString(),
                Hex  = ToBase(andDec, 16)
            });

            results.Add(new OperationResult {
                Item = "a OR b",
                Bin  = orBin,
                Oct  = ToBase(orDec, 8),
                Dec  = orDec.ToString(),
                Hex  = ToBase(orDec, 16)
            });

            results.Add(new OperationResult {
                Item = "a XOR b",
                Bin  = xorBin,
                Oct  = ToBase(xorDec, 8),
                Dec  = xorDec.ToString(),
                Hex  = ToBase(xorDec, 16)
            });

            // Operaciones aritméticas (en decimal) y luego cambio de base
            int sumDec = aDec + bDec;
            int mulDec = aDec * bDec;

            results.Add(new OperationResult {
                Item = "a + b",
                Bin  = ToBase(sumDec, 2),
                Oct  = ToBase(sumDec, 8),
                Dec  = sumDec.ToString(),
                Hex  = ToBase(sumDec, 16)
            });

            results.Add(new OperationResult {
                Item = "a • b",
                Bin  = ToBase(mulDec, 2),
                Oct  = ToBase(mulDec, 8),
                Dec  = mulDec.ToString(),
                Hex  = ToBase(mulDec, 16)
            });

            return results;
        }
    }
}
