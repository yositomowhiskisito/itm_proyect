using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LIBPresentationCore.Core
{
    public enum Loading { ADD, REMOVE }
    public enum Focus { DOWN, FIRST, NEXT }

    public interface IValidate
    {
        bool Validate();
    }

    public interface IClose
    {
        Task<Dictionary<string, object>> Close(Dictionary<string, object> data);
    }

    public interface IVmPopup<T>
    {
        T Current { get; set; }
        ObservableCollection<T> List { get; set; }

        Task Load();
        Task Cancel();
        Task Close();
    }

    public interface IVmMaintenance<T> : IValidate
    {
        T Current { get; set; }
        ObservableCollection<T> List { get; set; }

        Task New();
        Task Update();
        Task Load();
        Task Save();
        Task Search();
        Task Delete();
        Task Cancel();
        Task Close();
    }

    public interface IUserControl
    {
        void Close(bool noTab = true);
        void MoveFocus(Focus focus = Focus.FIRST);
        Task Loading(Loading action);
        void Detail(bool open = false);
    }

    public interface IUCPopup : IUserControl
    {
        void Show();
        object Selected { get; set; }
    }

    public interface ITile
    {
        object Tile { get; set; }
        Task<Dictionary<string, object>> Configure(Dictionary<string, object> data);
    }

    public interface IMenu
    {
        Task Open(object sender);
    }

    public interface IViewer
    {
        void Show(string path);
    }
}