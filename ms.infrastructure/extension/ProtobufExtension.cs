using Google.Protobuf.WellKnownTypes;
namespace Extension.ProtobufExtension
{
  /// <summary>
  /// 提供 Protobuf 相關的擴充方法。
  /// </summary>
  public static class ProtobufExtension
  {
    /// <summary>
    /// 將 <see cref="DateTime"/> 轉換為 <see cref="Timestamp"/>。
    /// </summary>
    /// <param name="dateTime">要轉換的 <see cref="DateTime"/> 物件。</param>
    /// <returns>轉換後的 <see cref="Timestamp"/> 物件。</returns>
    public static Timestamp ToTimestamp(this DateTime dateTime)
    {
      return Timestamp.FromDateTime(dateTime);
    }

    /// <summary>
    /// 將 <see cref="DateTimeOffset"/> 轉換為 <see cref="Timestamp"/>。
    /// </summary>
    /// <param name="dateTimeOffset">要轉換的 <see cref="DateTimeOffset"/> 物件。</param>
    /// <returns>轉換後的 <see cref="Timestamp"/> 物件。</returns>
    public static Timestamp ToTimestamp(this DateTimeOffset dateTimeOffset)
    {
      return Timestamp.FromDateTimeOffset(dateTimeOffset);
    }

    /// <summary>
    /// 將 <see cref="Timestamp"/> 轉換為 <see cref="DateTime"/>。
    /// </summary>
    /// <param name="timestamp">要轉換的 <see cref="Timestamp"/> 物件。</param>
    /// <returns>轉換後的 <see cref="DateTime"/> 物件。</returns>
    public static DateTime ToDateTime(this Timestamp timestamp)
    {
      return timestamp.ToDateTime();
    }

    /// <summary>
    /// 將 <see cref="Timestamp"/> 轉換為 <see cref="DateTimeOffset"/>。
    /// </summary>
    /// <param name="timestamp">要轉換的 <see cref="Timestamp"/> 物件。</param>
    /// <returns>轉換後的 <see cref="DateTimeOffset"/> 物件。</returns>
    public static DateTimeOffset ToDateTimeOffset(this Timestamp timestamp)
    {
      return timestamp.ToDateTimeOffset();
    }

    /// <summary>
    /// 將 <see cref="TimeSpan"/> 轉換為 <see cref="Duration"/>。
    /// </summary>
    /// <param name="timeSpan">要轉換的 <see cref="TimeSpan"/> 物件。</param>
    /// <returns>轉換後的 <see cref="Duration"/> 物件。</returns>
    public static Duration ToDuration(this TimeSpan timeSpan)
    {
      return Duration.FromTimeSpan(timeSpan);
    }

    /// <summary>
    /// 將 <see cref="Duration"/> 轉換為 <see cref="TimeSpan"/>。
    /// </summary>
    /// <param name="duration">要轉換的 <see cref="Duration"/> 物件。</param>
    /// <returns>轉換後的 <see cref="TimeSpan"/> 物件。</returns>
    public static TimeSpan ToTimeSpan(this Duration duration)
    {
      return duration.ToTimeSpan();
    }
  }
}