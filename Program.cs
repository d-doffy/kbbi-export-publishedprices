// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using static System.DateTime;

var dic = new Dictionary<string, Action<string>>()
{
    { "-S", dataSource => AppSettings.DataSource = dataSource },
    { "-U", userName =>  AppSettings.UserId = userName},
    { "-P", password => AppSettings.Password = password },
    { "-d", catalog => AppSettings.Catalog = catalog }
};

var i = 0;
for (; i < args.Length;) 
{
    if (args[i].StartsWith("-"))
    {
        if (dic.TryGetValue(args[i], out var action))
        {
            i++;
            action.Invoke(args[i]);
        }
    }
    i++;
}

PublishedPrices.GeneratePublishedPrices();

public static class AppSettings
{
    public static string DataSource = "";
    
    public static string Catalog = "";
    
    public static string UserId = "";
    
    public static string Password = "";
}

public class PublishedPrices
{
    [DisplayName("KBB_ID")] public int KBB_ID { get; set; }
    [DisplayName("ISLEAD")] public bool IsLead { get; set; }
    [DisplayName("AV_FORECAST_PRICE")] public int AV_Forecast_Price { get; set; }
    [DisplayName("AV_FORECAST_DATE")] public string AV_Forecast_Date { get; set; } = string.Empty;
    [DisplayName("AV_TYPICAL_MILEAGE")] public int AV_Typical_Mileage { get; set; }
    [DisplayName("AV_IMPUTATION")] public int AV_Imputation { get; set; }
    [DisplayName("AV_PUBLISHED_PRICE")] public int AV_Published_Price { get; set; }
    [DisplayName("TI_FORECAST_PRICE")] public int TI_Forecast_Price { get; set; }
    [DisplayName("TI_PUBLISHED_PRICE")] public int TI_Published_Price { get; set; }
    [DisplayName("TA_FORECAST_PRICE")] public int TA_Forecast_Price { get; set; }
    [DisplayName("TA_FORECAST_DATE")] public string TA_Forecast_Date { get; set; } = string.Empty;
    [DisplayName("TA_TYPICAL_MILEAGE")] public int TA_Typicale_Mileage { get; set; }
    [DisplayName("TA_IMPUTATION")] public int TA_Imputation { get; set; }
    [DisplayName("TA_PUBLISHED_PRICE")] public int TA_Published_Price { get; set; }
    [DisplayName("PP_FORECAST_PRICE")] public int PP_Forecast_Price { get; set; }
    [DisplayName("PP_PUBLISHED_PRICE")] public int PP_Published_Price { get; set; }
    [DisplayName("NEW_FPP")] public int New_FPP { get; set; }
    [DisplayName("NEW_DISCOUNT_LOW")] public string New_Discount_Low { get; set; } = string.Empty;
    [DisplayName("NEW_DISCOUNT_HIGH")] public string New_Discount_High { get; set; } = string.Empty;
    [DisplayName("PUBLISHED_DATE")] public string Published_Date { get; set; } = string.Empty;
    [DisplayName("CREATED_DATE")] public string Created_Date { get; set; } = string.Empty;
    [DisplayName("IsFullPublish")] public bool IsFullPublish { get; set; }


    public PublishedPrices()
    {
    }

    public PublishedPrices(IDataRecord? dr)
    {
        if (dr == null)
        {
            return;
        }

        KBB_ID = Convert.ToInt32(dr["KBB_ID"]);

        if (dr["IsLead"] != DBNull.Value)
            IsLead = Convert.ToBoolean(dr["IsLead"]);

        if (dr["AV_Forecast_Price"] != DBNull.Value)
            AV_Forecast_Price = (int)Convert.ToDouble(dr["AV_Forecast_Price"] + string.Empty);

        if (dr["AV_Forecast_Date"] != DBNull.Value)
        {
            if (TryParse(dr["AV_Forecast_Date"] + string.Empty, out DateTime _AV_Forecast_Date))
            {
                AV_Forecast_Date = _AV_Forecast_Date.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        if (dr["AV_Typical_Mileage"] != DBNull.Value)
            AV_Typical_Mileage = Convert.ToInt32(dr["AV_Typical_Mileage"] + string.Empty);

        if (dr["AV_Imputation"] != DBNull.Value)
            AV_Imputation = Convert.ToInt32(dr["AV_Imputation"] + string.Empty);

        if (dr["AV_Published_Price"] != DBNull.Value)
            AV_Published_Price = (int)Convert.ToDouble(dr["AV_Published_Price"] + string.Empty);

        if (dr["TI_Forecast_Price"] != DBNull.Value)
            TI_Forecast_Price = (int)Convert.ToDouble(dr["TI_Forecast_Price"] + string.Empty);

        if (dr["TI_Published_Price"] != DBNull.Value)
            TI_Published_Price = (int)Convert.ToDouble(dr["TI_Published_Price"] + string.Empty);

        if (dr["TA_Forecast_Price"] != DBNull.Value)
            TA_Forecast_Price = (int)Convert.ToDouble(dr["TA_Forecast_Price"] + string.Empty);

        if (dr["TA_Forecast_Date"] != DBNull.Value)
        {
            if (TryParse(dr["TA_Forecast_Date"] + string.Empty, out DateTime _TA_Forecast_Date))
            {
                TA_Forecast_Date = _TA_Forecast_Date.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        if (dr["TA_Typicale_Mileage"] != DBNull.Value)
            TA_Typicale_Mileage = Convert.ToInt32(dr["TA_Typicale_Mileage"] + string.Empty);

        if (dr["TA_Imputation"] != DBNull.Value)
            TA_Imputation = Convert.ToInt32(dr["TA_Imputation"] + string.Empty);

        if (dr["TA_Published_Price"] != DBNull.Value)
            TA_Published_Price = (int)Convert.ToDouble(dr["TA_Published_Price"] + string.Empty);

        if (dr["PP_Forecast_Price"] != DBNull.Value)
            PP_Forecast_Price = (int)Convert.ToDouble(dr["PP_Forecast_Price"] + string.Empty);

        if (dr["PP_Published_Price"] != DBNull.Value)
            PP_Published_Price = (int)Convert.ToDouble(dr["PP_Published_Price"] + string.Empty);

        if (dr["New_FPP"] != DBNull.Value)
            New_FPP = Convert.ToInt32(dr["New_FPP"] + string.Empty);

        if (dr["New_Discount_Low"] != DBNull.Value)
            New_Discount_Low = dr["New_Discount_Low"] + string.Empty;

        if (dr["New_Discount_High"] != DBNull.Value)
            New_Discount_High = dr["New_Discount_High"] + string.Empty;

        if (dr["Published_Date"] != DBNull.Value)
        {
            if (TryParse(dr["Published_Date"] + string.Empty, out DateTime _Published_Date))
            {
                Published_Date = _Published_Date.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        if (dr["Created_Date"] != DBNull.Value)
        {
            if (TryParse(dr["Created_Date"] + string.Empty, out DateTime _Created_Date))
            {
                Created_Date = _Created_Date.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        if (dr["IsFullPublish"] != DBNull.Value)
            IsFullPublish = Convert.ToBoolean(dr["IsFullPublish"]);
    }

    public static string GeneratePublishedPrices()
    {
        var fileNameDate = UtcNow.ToString("yyyyMMddHHmmss");
        var fileName = $"PublishedPrices_{fileNameDate}_en-CA_processed.csv";


        using var writer = new StreamWriter(fileName, true, Encoding.Unicode);
        using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";"
        });
        csv.WriteRecords(GetAll());
        writer.Flush();
        csv.Flush();
        return fileName;
    }

    private static IEnumerable<PublishedPrices> GetAll()
    {
        var list = new List<PublishedPrices>();

        var sqlBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = AppSettings.DataSource,
            InitialCatalog = AppSettings.Catalog,
            UserID = AppSettings.UserId,
            Password = AppSettings.Password
        };

        using var sqlConn = new SqlConnection(sqlBuilder.ConnectionString);

        if (sqlConn.State == ConnectionState.Closed)
        {
            sqlConn.Open();
        }

        const string cmdText = "DataReference_GetVehiclePublishedPrices";
        var command = new SqlCommand(cmdText, sqlConn)
        {
            CommandType = CommandType.StoredProcedure
        };

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new PublishedPrices(reader));
        }

        return list;
    }
}