using Dapper;
using System.Data;

namespace MManager.DateHand
{
    public class DateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public override DateTimeOffset Parse(object value) => DateTimeOffset.FromUnixTimeSeconds((long)value).ToLocalTime();
        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value) => parameter.Value = value;
    }
}
