using System;

namespace AngryWasp.Helpers
{
    public class DateTimeHelper
    {
        /// <summary>
        /// Gets the current UTC time as a Unix timestamp
        /// </summary>
        /// <returns>The current UTC time as a Unix timestamp</returns>
        public static ulong TimestampNow()
        {
            TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            return (ulong)span.TotalSeconds;
        }

        /// <summary>
        /// Converts a Unix timestamp to a DateTime structure
        /// </summary>
        /// <param name="ts">The timestamp to convert</param>
        /// <returns>A DateTime representing the timestamp</returns>
        public static DateTime UnixTimestampToDateTime(ulong ts)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dt.AddSeconds(ts).ToLocalTime();
        }

        /// <summary>
        /// Converts a DateTime to a Timestamp
        /// </summary>
        /// <param name="dt">The DateTime to convert</param>
        /// <returns>the unix timestamp representation of dt</returns>
        /// <remarks>The DateTime object should be a UTC DateTime object</remarks>
        public static ulong DateTimeToUnixTimestamp(DateTime dt)
        {
            TimeSpan span = (dt - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            return (ulong)span.TotalSeconds;
        }
    }
}