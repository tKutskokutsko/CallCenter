namespace CallCenterApi.Infrastructure.DB;

public interface IDatabaseService
{
    void SaveData(string data);
    string RetrieveData();
    void Connect();
}