using System.Collections.Generic;
using ProgramDoObslugiZamowien.Model;

namespace ProgramDoObslugiZamowien.UI.Repository
{
    public interface IDataRepository
    {
        void AddData(string fullPathName, List<Request> requests);
    }
}