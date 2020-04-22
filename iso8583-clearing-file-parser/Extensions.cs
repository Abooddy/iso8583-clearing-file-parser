using System;

namespace iso8583_clearing_file_parser
{
    static class Extensions
    {
        public static string PostionParser(this string data, ref int postion, int length, bool isLengthPrepended = false, bool increasePostion = true)
        {
            var parsedData = string.Empty;

            if (isLengthPrepended)
            {
                int prependedLength = Convert.ToInt32(data.Substring(postion, length));

                parsedData = data.Substring(postion + length, prependedLength).Trim();

                if (increasePostion)
                    postion += length + prependedLength;
            }

            else
            {
                parsedData = data.Substring(postion, length).Trim();

                if (increasePostion)
                    postion += length;
            }

            return parsedData;
        }
    }
}
