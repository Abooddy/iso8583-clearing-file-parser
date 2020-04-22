using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iso8583_clearing_file_parser
{
    public class ParserClearingFile
    {
        public static List<ISO8583Message> Parse(List<byte> clearingFile, Encoding fileEncoding)
        {
            var fileArray = ParserISO8583.PrepareFileByteArray(clearingFile);

            var readableEncoding = Encoding.GetEncoding("Windows-1252");

            if (fileEncoding != Encoding.GetEncoding("Windows-1252"))
            {
                readableEncoding = Encoding.GetEncoding("IBM037");
            }

            try
            {
                int postion = 0;
                long calcChecksum = 0;
                string functionCode = string.Empty;
                List<string[]> isoMessages = new List<string[]>();

                while (true)
                {
                    if (functionCode == "695")
                    {
                        var fileChecksum = Convert.ToInt64(ParserISO8583.GetPds(isoMessages.Last()[48], "0301"));

                        if (fileChecksum != calcChecksum)
                        {
                            throw new Exception("Invalid checksum");
                        }

                        return ParserISO8583.FillEntities(isoMessages);
                    }

                    byte[] length = new byte[4];
                    byte[] mti = new byte[4];
                    byte[] bitmap = new byte[16];

                    Array.Copy(fileArray, 0 + postion, length, 0, 4);
                    Array.Copy(fileArray, 4 + postion, mti, 0, 4);
                    Array.Copy(fileArray, 8 + postion, bitmap, 0, 16);

                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(length);

                    int l = BitConverter.ToInt32(length, 0);

                    byte[] data = new byte[l - 20];

                    Array.Copy(fileArray, 24 + postion, data, 0, l - 20);

                    postion += l + 4;

                    var stringMti = fileEncoding.GetString(Encoding.Convert(readableEncoding, fileEncoding, mti));
                    var stringData = fileEncoding.GetString(Encoding.Convert(readableEncoding, fileEncoding, data));
                    var priBitmap = BitConverter.ToString(bitmap).Replace("-", "").Substring(0, 16);
                    var secBitmap = BitConverter.ToString(bitmap).Replace("-", "").Substring(16, 16);

                    var parsedIsoMessage = new string[128];

                    try
                    {
                        parsedIsoMessage = ParserISO8583.Parse(stringData, ParserISO8583.GetBitmapBinaryString(bitmap));
                    }

                    catch (Exception ex)
                    {
                        throw new Exception($"Error while parsing message number {isoMessages.Count()}, the original error was: {ex.Message}");
                    }

                    parsedIsoMessage[0] = stringMti;
                    parsedIsoMessage[1] = priBitmap + secBitmap;

                    isoMessages.Add(parsedIsoMessage);

                    functionCode = isoMessages.Last().ToArray()[24];
                    calcChecksum += Convert.ToInt64(isoMessages.Last()[4] ?? "0");
                }
            }

            catch (Exception ex)
            {
                throw new Exception("Parser error: " + ex.Message);
            }
        }
    }
}
