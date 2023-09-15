using LIBDomainEntities.Entities;
using LIBPresentationContext.Core;
using LIBPresentationContext.Implementations.Helpers.Communications;
using LIBPresentationCore.Core;
using LIBPresentationCore.Interfaces.IVwModels;
using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Action = LIBUtilities.Core.Action;

namespace LIBPresentationContext.Implementations.VwModels
{
    public class VmPersons : VModel<Persons>
    {
        private IUCPopup IUCPopup;

        public VmPersons(Dictionary<string, object> data) : base(data)
        {
            try
            {
                IHelper = new PersonsHelper();
                FileCommand = new EventsCommand(FileCommandExecute);
                SelectedCommand = new EventsCommand(SelectedCommandExecute);
                SetData(data);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        private void SetData(Dictionary<string, object> data)
        {
            try
            {
                if (data == null)
                    return;
                if (data.ContainsKey("View"))
                    IUCPopup = (IUCPopup)data["View"];
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        public EventsCommand FileCommand { get; private set; }
        public EventsCommand SelectedCommand { get; private set; }

        public override void Resources()
        {
            try
            {
                lbSelectItem = "RsMessages.lbSelectItem";
                lbDeleteEntity = "RsPersons.lbDeleteEntity";
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public override bool FillEntity()
        {
            return true;
        }

        public override async Task Cancel()
        {
            var temp = Action.NONE;
            try
            {
                if (currentCopy == null)
                    return;

                Current.Name = currentCopy.Name;
                currentCopy = null;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
            finally
            {
                if (action == Action.NEW)
                    current = null;
                Action = temp;
            }
        }

        public override void CreateCopy(Action action)
        {
            try
            {
                if (Current == null)
                    return;

                currentCopy = (Persons)Current.Clone();
                Action = action;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
                Action = LIBUtilities.Core.Action.NONE;
            }
        }

        public Dictionary<string, object> File(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                if (Current == null)
                    return response;

                Current.File = FileHelper.Load();
                return response;
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
                return response;
            }
        }

        public async Task<Dictionary<string, object>> Selected(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                await IUCPopup.Loading(Loading.ADD);
                if (Current != null)
                    IUCPopup.Selected = Current;
                IUCPopup.Close();
                return response;
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);

                response["Error"] = ex.ToString();
                return response;
            }
            finally
            {
                await IUserControl.Loading(Loading.REMOVE);
            }
        }

        public void FileCommandExecute(object parameter) { File(null); }
        public void SelectedCommandExecute(object parameter) { Selected(null); }
    }
}