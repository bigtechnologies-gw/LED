namespace EnaApp.Models
{
    internal interface IDataPersistent
    {
        void LoadState();

        void SaveState();
    }
}