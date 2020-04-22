using System;
using System.Collections.Generic;
using System.Linq;

namespace iso8583_clearing_file_parser
{
    public class ParserISO8583
    {
        public static string[] Parse(string message, string bitmap)
        {
            int postion = 0;

            string[] parsedMessage = new string[129];

            for (int i = 2; i < bitmap.Length; i++)
            {
                if (bitmap.Substring(i - 1, 1) == "1")
                {
                    try
                    {
                        GetElementSpecs(i, out int length, out bool isLengthPrepended);

                        parsedMessage[i] = message.PostionParser(ref postion, length, isLengthPrepended);
                    }

                    catch
                    {
                        throw new Exception("Error in parsing DE[" + i + "]");
                    }
                }
            }

            return parsedMessage;
        }

        public static byte[] PrepareFileByteArray(List<byte> fileByteArray)
        {
            #region Remove all unused byte couples found at the end of each 1014 byte.
            int nullsRemoved = 0;

            for (int i = 1; i < fileByteArray.Count; i++)
            {
                if (i % 1014 == 0)
                {
                    fileByteArray.RemoveAt(i - 1);
                    fileByteArray.RemoveAt(i - 2);

                    // To keep the length of the array the same after the removal.
                    fileByteArray.Insert(0, 0);
                    fileByteArray.Insert(0, 0);

                    nullsRemoved += 2;
                }
            }

            fileByteArray.RemoveRange(0, nullsRemoved);
            #endregion

            var fileArray = fileByteArray.ToArray();

            #region Removes trailing zero bytes.
            int lastIndex = Array.FindLastIndex(fileArray, b => b != 0);

            Array.Resize(ref fileArray, lastIndex + 1);
            #endregion

            return fileArray;
        }

        public static string GetBitmapBinaryString(byte[] bitmap)
        {
            return string.Join("", bitmap.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
        }

        public static List<ISO8583Message> FillEntities(List<string[]> parsedISOMessages)
        {
            List<ISO8583Message> entities = new List<ISO8583Message>();

            foreach (var isoMessage in parsedISOMessages)
            {
                ISO8583Message rec = new ISO8583Message()
                {
                    MTI = isoMessage[0],
                    BITMAP = isoMessage[1],
                    DE02 = isoMessage[2],
                    DE03 = isoMessage[3],
                    DE04 = isoMessage[4],
                    DE05 = isoMessage[5],
                    DE06 = isoMessage[6],
                    DE07 = isoMessage[7],
                    DE08 = isoMessage[8],
                    DE09 = isoMessage[9],
                    DE10 = isoMessage[10],
                    DE11 = isoMessage[11],
                    DE12 = isoMessage[12],
                    DE13 = isoMessage[13],
                    DE14 = isoMessage[14],
                    DE15 = isoMessage[15],
                    DE16 = isoMessage[16],
                    DE17 = isoMessage[17],
                    DE18 = isoMessage[18],
                    DE19 = isoMessage[19],
                    DE20 = isoMessage[20],
                    DE21 = isoMessage[21],
                    DE22 = isoMessage[22],
                    DE23 = isoMessage[23],
                    DE24 = isoMessage[24],
                    DE25 = isoMessage[25],
                    DE26 = isoMessage[26],
                    DE27 = isoMessage[27],
                    DE28 = isoMessage[28],
                    DE29 = isoMessage[29],
                    DE30 = isoMessage[30],
                    DE31 = isoMessage[31],
                    DE32 = isoMessage[32],
                    DE33 = isoMessage[33],
                    DE34 = isoMessage[34],
                    DE35 = isoMessage[35],
                    DE36 = isoMessage[36],
                    DE37 = isoMessage[37],
                    DE38 = isoMessage[38],
                    DE39 = isoMessage[39],
                    DE40 = isoMessage[40],
                    DE41 = isoMessage[41],
                    DE42 = isoMessage[42],
                    DE43 = isoMessage[43],
                    DE44 = isoMessage[44],
                    DE45 = isoMessage[45],
                    DE46 = isoMessage[46],
                    DE47 = isoMessage[47],
                    DE48 = isoMessage[48],
                    DE49 = isoMessage[49],
                    DE50 = isoMessage[50],
                    DE51 = isoMessage[51],
                    DE52 = isoMessage[52],
                    DE53 = isoMessage[53],
                    DE54 = isoMessage[54],
                    DE55 = isoMessage[55],
                    DE56 = isoMessage[56],
                    DE57 = isoMessage[57],
                    DE58 = isoMessage[58],
                    DE59 = isoMessage[59],
                    DE60 = isoMessage[60],
                    DE61 = isoMessage[61],
                    DE62 = isoMessage[62],
                    DE63 = isoMessage[63],
                    DE64 = isoMessage[64],
                    DE65 = isoMessage[65],
                    DE66 = isoMessage[66],
                    DE67 = isoMessage[67],
                    DE68 = isoMessage[68],
                    DE69 = isoMessage[69],
                    DE70 = isoMessage[70],
                    DE71 = isoMessage[71],
                    DE72 = isoMessage[72],
                    DE73 = isoMessage[73],
                    DE74 = isoMessage[74],
                    DE75 = isoMessage[75],
                    DE76 = isoMessage[76],
                    DE77 = isoMessage[77],
                    DE78 = isoMessage[78],
                    DE79 = isoMessage[79],
                    DE80 = isoMessage[80],
                    DE81 = isoMessage[81],
                    DE82 = isoMessage[82],
                    DE83 = isoMessage[83],
                    DE84 = isoMessage[84],
                    DE85 = isoMessage[85],
                    DE86 = isoMessage[86],
                    DE87 = isoMessage[87],
                    DE88 = isoMessage[88],
                    DE89 = isoMessage[89],
                    DE90 = isoMessage[90],
                    DE91 = isoMessage[91],
                    DE92 = isoMessage[92],
                    DE93 = isoMessage[93],
                    DE94 = isoMessage[94],
                    DE95 = isoMessage[95],
                    DE96 = isoMessage[96],
                    DE97 = isoMessage[97],
                    DE98 = isoMessage[98],
                    DE99 = isoMessage[99],
                    DE100 = isoMessage[100],
                    DE101 = isoMessage[101],
                    DE102 = isoMessage[102],
                    DE103 = isoMessage[103],
                    DE104 = isoMessage[104],
                    DE105 = isoMessage[105],
                    DE106 = isoMessage[106],
                    DE107 = isoMessage[107],
                    DE108 = isoMessage[108],
                    DE109 = isoMessage[109],
                    DE110 = isoMessage[110],
                    DE111 = isoMessage[111],
                    DE112 = isoMessage[112],
                    DE113 = isoMessage[113],
                    DE114 = isoMessage[114],
                    DE115 = isoMessage[115],
                    DE116 = isoMessage[116],
                    DE117 = isoMessage[117],
                    DE118 = isoMessage[118],
                    DE119 = isoMessage[119],
                    DE120 = isoMessage[120],
                    DE121 = isoMessage[121],
                    DE122 = isoMessage[122],
                    DE123 = isoMessage[123],
                    DE124 = isoMessage[124],
                    DE125 = isoMessage[125],
                    DE126 = isoMessage[126],
                    DE127 = isoMessage[127],
                    DE128 = isoMessage[128]
                };

                entities.Add(rec);
            }

            return entities;
        }

        public static string GetPds(string de48, string pds)
        {
            int pos = 0;

            while (pos < de48.Length)
            {
                string currentPds = de48.Substring(pos, 4);
                int pdsLength = Convert.ToInt32(de48.Substring(pos + 4, 3) ?? "0");
                string pdsData = de48.Substring(pos + 7, pdsLength);

                pos = pos + 7 + pdsLength;

                if (currentPds == pds)
                {
                    return pdsData;
                }
            }

            return null;
        }

        private static void GetElementSpecs(int elementIndex,
                                            out int length,
                                            out bool isLengthPrepended)
        {
            isLengthPrepended = false;

            switch (elementIndex)
            {
                case 2: { length = 2; isLengthPrepended = true; break; }
                case 3: { length = 6; break; }
                case 4: { length = 12; break; }
                case 5: { length = 12; break; }
                case 6: { length = 12; break; }
                case 7: { length = 10; break; }
                case 8: { length = 8; break; }
                case 9: { length = 8; break; }
                case 10: { length = 8; break; }
                case 11: { length = 6; break; }
                case 12: { length = 12; break; }
                case 13: { length = 4; break; }
                case 14: { length = 4; break; }
                case 15: { length = 6; break; }
                case 16: { length = 4; break; }
                case 17: { length = 4; break; }
                case 18: { length = 4; break; }
                case 19: { length = 3; break; }
                case 20: { length = 3; break; }
                case 21: { length = 3; break; }
                case 22: { length = 12; break; }
                case 23: { length = 3; break; }
                case 24: { length = 3; break; }
                case 25: { length = 4; break; }
                case 26: { length = 4; break; }
                case 27: { length = 1; break; }
                case 28: { length = 6; break; }
                case 29: { length = 3; break; }
                case 30: { length = 24; break; }
                //case 31: { length = 8; break; }
                case 31: { length = 2; isLengthPrepended = true; break; }
                case 32: { length = 2; isLengthPrepended = true; break; }
                case 33: { length = 2; isLengthPrepended = true; break; }
                case 34: { length = 2; isLengthPrepended = true; break; }
                case 35: { length = 2; isLengthPrepended = true; break; }
                case 36: { length = 3; isLengthPrepended = true; break; }
                case 37: { length = 12; break; }
                case 38: { length = 6; break; }
                case 39: { length = 3; break; }
                case 40: { length = 3; break; }
                case 41: { length = 8; break; }
                case 42: { length = 15; break; }
                //case 43: { length = 40; break; }
                case 43: { length = 2; isLengthPrepended = true; break; }
                case 44: { length = 2; isLengthPrepended = true; break; }
                case 45: { length = 2; isLengthPrepended = true; break; }
                case 46: { length = 3; isLengthPrepended = true; break; }
                case 47: { length = 3; isLengthPrepended = true; break; }
                case 48: { length = 3; isLengthPrepended = true; break; }
                case 49: { length = 3; break; }
                case 50: { length = 3; break; }
                case 51: { length = 3; break; }
                case 52: { length = 8; break; }
                case 53: { length = 16; break; }
                case 54: { length = 3; isLengthPrepended = true; break; }
                case 55: { length = 3; isLengthPrepended = true; break; }
                case 56: { length = 2; isLengthPrepended = true; break; }
                case 57: { length = 3; break; }
                case 58: { length = 2; isLengthPrepended = true; break; }
                case 59: { length = 3; isLengthPrepended = true; break; }
                case 60: { length = 3; isLengthPrepended = true; break; }
                case 61: { length = 3; isLengthPrepended = true; break; }
                case 62: { length = 3; isLengthPrepended = true; break; }
                //case 63: { length = 12; break; }
                case 63: { length = 3; isLengthPrepended = true; break; }
                case 64: { length = 8; break; }
                case 65: { length = 8; break; }
                case 66: { length = 3; isLengthPrepended = true; break; }
                case 67: { length = 2; break; }
                case 68: { length = 3; break; }
                case 69: { length = 3; break; }
                case 70: { length = 3; break; }
                case 71: { length = 8; break; }
                case 72: { length = 3; isLengthPrepended = true; break; }
                case 73: { length = 6; break; }
                case 74: { length = 10; break; }
                case 75: { length = 10; break; }
                case 76: { length = 10; break; }
                case 77: { length = 10; break; }
                case 78: { length = 10; break; }
                case 79: { length = 10; break; }
                case 80: { length = 10; break; }
                case 81: { length = 10; break; }
                case 82: { length = 10; break; }
                case 83: { length = 10; break; }
                case 84: { length = 10; break; }
                case 85: { length = 10; break; }
                case 86: { length = 16; break; }
                case 87: { length = 16; break; }
                case 88: { length = 16; break; }
                case 89: { length = 16; break; }
                case 90: { length = 10; break; }
                case 91: { length = 3; break; }
                case 92: { length = 3; break; }
                case 93: { length = 2; isLengthPrepended = true; break; }
                case 94: { length = 2; isLengthPrepended = true; break; }
                //case 95: { length = 3; isLengthPrepended = true; break; }
                case 95: { length = 12; break; }
                case 96: { length = 3; isLengthPrepended = true; break; }
                case 97: { length = 17; break; }
                case 98: { length = 25; break; }
                case 99: { length = 2; isLengthPrepended = true; break; }
                case 100: { length = 2; isLengthPrepended = true; break; }
                case 101: { length = 2; isLengthPrepended = true; break; }
                case 102: { length = 2; isLengthPrepended = true; break; }
                case 103: { length = 2; isLengthPrepended = true; break; }
                case 104: { length = 3; isLengthPrepended = true; break; }
                case 105: { length = 16; break; }
                case 106: { length = 16; break; }
                case 107: { length = 10; break; }
                case 108: { length = 10; break; }
                case 109: { length = 2; isLengthPrepended = true; break; }
                case 110: { length = 2; isLengthPrepended = true; break; }
                case 111: { length = 3; isLengthPrepended = true; break; }
                case 112: { length = 3; isLengthPrepended = true; break; }
                case 113: { length = 3; isLengthPrepended = true; break; }
                case 114: { length = 3; isLengthPrepended = true; break; }
                case 115: { length = 3; isLengthPrepended = true; break; }
                case 116: { length = 3; isLengthPrepended = true; break; }
                case 117: { length = 3; isLengthPrepended = true; break; }
                case 118: { length = 3; isLengthPrepended = true; break; }
                case 119: { length = 3; isLengthPrepended = true; break; }
                case 120: { length = 3; isLengthPrepended = true; break; }
                case 121: { length = 3; isLengthPrepended = true; break; }
                case 122: { length = 3; isLengthPrepended = true; break; }
                case 123: { length = 3; isLengthPrepended = true; break; }
                case 124: { length = 3; isLengthPrepended = true; break; }
                case 125: { length = 3; isLengthPrepended = true; break; }
                case 126: { length = 3; isLengthPrepended = true; break; }
                case 127: { length = 3; isLengthPrepended = true; break; }
                case 128: { length = 8; break; }

                default:
                    throw new Exception("Invalid data element DE[" + elementIndex + "]");
            }
        }
    }
}
