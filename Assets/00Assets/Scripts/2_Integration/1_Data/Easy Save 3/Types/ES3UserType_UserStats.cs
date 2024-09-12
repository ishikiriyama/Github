namespace ES3Types
{
    [UnityEngine.Scripting.Preserve]
    [ES3PropertiesAttribute("<Level>k__BackingField", "<Money>k__BackingField")]
    public class ES3UserType_UserStats : ES3Type
    {
        public static ES3Type Instance = null;

        public ES3UserType_UserStats() : base(typeof(Projects.Core.Domain.User.UserStats)) { Instance = this; priority = 1; }


        public override void Write(object obj, ES3Writer writer)
        {
            var instance = (Projects.Core.Domain.User.UserStats)obj;

            writer.WritePrivateField("<Level>k__BackingField", instance);
            writer.WritePrivateField("<Money>k__BackingField", instance);
        }

        public override object Read<T>(ES3Reader reader)
        {
            var instance = new Projects.Core.Domain.User.UserStats();
            string propertyName;
            while ((propertyName = reader.ReadPropertyName()) != null)
            {
                switch (propertyName)
                {

                    case "<Level>k__BackingField":
                        instance = (Projects.Core.Domain.User.UserStats)reader.SetPrivateField("<Level>k__BackingField", reader.Read<System.Int32>(), instance);
                        break;
                    case "<Money>k__BackingField":
                        instance = (Projects.Core.Domain.User.UserStats)reader.SetPrivateField("<Money>k__BackingField", reader.Read<System.Int32>(), instance);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
            return instance;
        }
    }


    public class ES3UserType_UserStatsArray : ES3ArrayType
    {
        public static ES3Type Instance;

        public ES3UserType_UserStatsArray() : base(typeof(Projects.Core.Domain.User.UserStats[]), ES3UserType_UserStats.Instance)
        {
            Instance = this;
        }
    }
}