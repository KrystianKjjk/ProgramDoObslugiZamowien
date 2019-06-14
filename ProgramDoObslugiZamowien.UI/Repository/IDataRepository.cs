using System.Collections.Generic;
using ProgramDoObslugiZamowien.Model;

namespace ProgramDoObslugiZamowien.UI.Repository
{
    public interface IDataRepository
    {
        List<Request> GetData(string fullFilePath);
        void AddData(string fullPathName, List<Request> requests);
        void RemoveData(string fullFiePath);
    }
}