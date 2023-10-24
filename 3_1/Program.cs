using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        object[][] events = new object[][]
        {
            new object[] {10, "2023/10/24", "2023/11/7"},
            new object[] {11, "2023/12/12", "2023/12/25"},
            new object[] {0, "2023/12/30", "2024/1/16"},
            new object[] {1, "2024/2/6", "2024/2/20"},
            new object[] {2, "2024/2/21", "2024/3/6"},
            new object[] {3, "2024/3/7", "2024/3/20"},
            new object[] {4, "2024/3/21", "2024/4/10"},
            new object[] {5, "2024/4/25", "2024/5/9"},
            new object[] {6, "2024/6/6", "2024/6/24"},
            new object[] {7, "2024/7/4", "2024/7/18"},
            new object[] {8, "2024/7/18", "2024/8/1"},
            new object[] {7, "2024/8/15", "2024/9/5"},
            new object[] {12, "2024/9/25", "2024/10/23"},
            new object[] {13, "2024/8/16", "2024/8/23"},
            new object[] {14, "2024/4/11", "2024/4/18"},
            new object[] {16, "2024/6/3", "2024/6/5"},
            new object[] {17, "2024/1/17", "2024/1/24"},
            new object[] {9, "2024/9/6", "2024/9/19" }
        };

        using (MemoryStream ms = new MemoryStream())
        {
            foreach (var eventData in events)
            {
                int eventId = (int)eventData[0];
                byte[] eventIdBytes = BitConverter.GetBytes(eventId);
                ms.Write(eventIdBytes, 0, eventIdBytes.Length);

                for (int i = 1; i <= 2; i++)
                {
                    DateTime date = DateTime.Parse((string)eventData[i]);

                    if (i == 1) // 開始日の場合
                        date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                    else       // 終了日の場合
                        date = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

                    int timestamp = (int)(date - new DateTime(1970, 1, 1)).TotalSeconds;
                    byte[] dateBytes = BitConverter.GetBytes(timestamp);
                    ms.Write(dateBytes, 0, dateBytes.Length);
                }
            }

            byte[] result = ms.ToArray();
            File.WriteAllBytes("rezz.bin", result);
            foreach (byte b in result)
            {
                Console.Write(b.ToString("X2") + " "); // バイトを16進数で出力
            }

        }
    }
}
