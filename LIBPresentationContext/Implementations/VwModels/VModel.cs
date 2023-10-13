using LIBDomainContext;
using LIBPresentationCore.Core;
using LibUtilities.Core;
using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Action = LIBUtilities.Core.Action;

namespace LIBPresentationContext.Implementations.VwModels
{
    public abstract class VModel<T> : Bindable, IVmMaintenance<T> where T : class, new()
    {
        protected T current, currentCopy, currentFilter;
        protected ObservableCollection<T> list, copy;
        protected IUserControl IUserControl;
        protected Action action;
        protected IHelper IHelper;
        protected int fontSize = 16;
        protected string lbSelectItem, lbDeleteEntity;

        public VModel(Dictionary<string, object> data)
        {
            try
            {
                if (data.ContainsKey("View"))
                    IUserControl = (IUserControl)data["View"];
                CurrentFilter = new T();
                InitializeCommands();
                Resources();
                if (data.ContainsKey("IHelper"))
                    IHelper = (IHelper)data["IHelper"];
                List = new ObservableCollection<T>();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public virtual T Current
        {
            get { return current; }
            set { SetProperty(ref current, value); }
        }
        public virtual ObservableCollection<T> List
        {
            get { return list; }
            set { SetProperty(ref list, value); }
        }
        public virtual Action Action
        {
            get { return action; }
            set { SetProperty(ref action, value); }
        }
        public virtual T CurrentFilter
        {
            get { return currentFilter; }
            set { SetProperty(ref currentFilter, value); }
        }
        public int FontSize
        {
            get { return fontSize; }
            set { SetProperty(ref fontSize, value); }
        }
        public EventsCommand LoadCommand { get; private set; }
        public EventsCommand NewCommand { get; private set; }
        public EventsCommand SaveCommand { get; private set; }
        public EventsCommand UpdateCommand { get; private set; }
        public EventsCommand DeleteCommand { get; private set; }
        public EventsCommand CancelCommand { get; private set; }
        public EventsCommand CloseCommand { get; private set; }
        public EventsCommand SearchCommand { get; private set; }

        public virtual void CreateCopy(Action action) { }
        public virtual void Resources() { }
        public virtual bool FillEntity() { return true; }
        public virtual bool Compare(T t1, T t2) { return false; }

        public virtual void InitializeCommands()
        {
            try
            {
                LoadCommand = new EventsCommand(LoadCommandExecute);
                SaveCommand = new EventsCommand(SaveCommandExecute);
                UpdateCommand = new EventsCommand(UpdateCommandExecute);
                DeleteCommand = new EventsCommand(DeleteCommandExecute);
                NewCommand = new EventsCommand(NewCommandExecute);
                CancelCommand = new EventsCommand(CancelCommandExecute);
                CloseCommand = new EventsCommand(CloseCommandExecute);
                SearchCommand = new EventsCommand(SearchCommandExecute);
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        public virtual async Task Cancel() { }

        public virtual async Task Close()
        {
            try
            {
                IUserControl.Close();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public virtual async Task Delete()
        {
            try
            {
                if (Current == null)
                {
                    await MessagesHelper.AsyncShow(lbSelectItem);
                    return;
                }

                if (!(bool)await MessagesHelper.AskAsync(lbDeleteEntity))
                    return;

                await IUserControl.Loading(Loading.ADD);
                var data = new Dictionary<string, object>();
                data.Add("Current", Current);
                var response = await IHelper.DeleteEntity(data);
                if (response == null ||
                    response.ContainsKey("Error") ||
                    !response.ContainsKey("Response"))
                    return;

                if (Current != null)
                    List.Remove(Current);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
            finally
            {
                await IUserControl.Loading(Loading.REMOVE);
            }
        }

        public virtual async Task Load()
        {
            try
            {
                await IUserControl.Loading(Loading.ADD);
                var data = new Dictionary<string, object>();
                var response = await IHelper.GetList(data);
                if (response == null ||
                    response.ContainsKey("Error") ||
                    !response.ContainsKey("Entities"))
                    return;
                List.Clear();
                var temp = (List<T>)response["Entities"];
                foreach (var item in temp)
                    List.Add(item);
                //Current = List.FirstOrDefault();
                copy = new ObservableCollection<T>(List.ToList());
                await Cancel();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
            finally
            {
                await IUserControl.Loading(Loading.REMOVE);
            }
        }

        public virtual async Task New()
        {
            try
            {
                IUserControl.Detail(true);
                Current = new T();
                Action = Action.NEW;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public virtual async Task Save()
        {
            try
            {
                IUserControl.MoveFocus(Focus.DOWN);
                await IUserControl.Loading(Loading.ADD);
                var data = new Dictionary<string, object>();
                if (!FillEntity())
                    return;
                data.Add("Current", Current);
                if (Action == Action.NEW)
                {
                    var response = await IHelper.SaveEntity(data);

                    if (response == null ||
                        response.ContainsKey("Error") ||
                        !response.ContainsKey("Response"))
                        return;

                    Current = (T)response["Current"];
                    List.Add(Current);
                }
                else
                {
                    var response = await IHelper.UpdateEntity(data);

                    await MessagesHelper.AsyncShow("Info Saved");

                    if (response == null ||
                        response.ContainsKey("Error") ||
                        !response.ContainsKey("Response") ||
                        Convert.ToInt32(response["Response"]) != (int)Action.UPDATED)
                        return;
                    currentCopy = null;
                }
                Action = Action.NONE;
                //await IUserControl.ActiveButtons(Action);

                await Cancel();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
            finally
            {
                await IUserControl.Loading(Loading.REMOVE);
            }
        }

        public virtual async Task Search()
        {
            try
            {
                IUserControl.MoveFocus();
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public virtual async Task Update()
        {
            try
            {
                if (Action != Action.NONE)
                    return;

                if (Current == null)
                {
                    await MessagesHelper.AsyncShow(lbSelectItem);
                    return;
                }
                CreateCopy(Action.MODIFY);
                IUserControl.Detail(true);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public virtual bool Validate()
        {
            try
            {
                if (Action == Action.NONE)
                    return true;

                CancelCommand.Execute(null);
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
                return false;
            }
        }

        public virtual async void NewCommandExecute(object parameter) { await New(); }
        public virtual async void UpdateCommandExecute(object parameter) { await Update(); }
        public virtual async void LoadCommandExecute(object parameter) { await Load(); }
        public virtual async void SaveCommandExecute(object parameter) { await Save(); }
        public virtual async void DeleteCommandExecute(object parameter) { await Delete(); }
        public virtual async void CancelCommandExecute(object parameter) { await Cancel(); }
        public virtual async void CloseCommandExecute(object parameter) { await Close(); }
        public virtual async void SearchCommandExecute(object parameter) { await Search(); }
    }
}