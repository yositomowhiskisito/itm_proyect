using LIBDomainEntities.Entities;
using LIBPresentationContext.Core;
using LIBPresentationContext.Implementations.Helpers.Communications;
using LIBPresentationCore.Core;
using LIBPresentationCore.Interfaces.IVwModels;
using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Action = LIBUtilities.Core.Action;

namespace LIBPresentationContext.Implementations.VwModels
{
    public class VmUsers : VModel<Users>
    {
        public VmUsers(Dictionary<string, object> data) : base(data)
        {
            try
            {
                IHelper = new UsersHelper();
                LoginCommand = new EventsCommand(LoginCommandExecute);
                IUserControl = (IUserControl)data["Screen"];
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public EventsCommand LoginCommand { get; private set; }


        public override void Resources()
        {
            try
            {
                lbSelectItem = "RsMessages.lbSelectItem";
                lbDeleteEntity = "RsUsers.lbDeleteEntity";
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

        public async Task<Dictionary<string, object>> Login(Dictionary<string, object> data)
        {
            var response = new Dictionary<string, object>();
            try
            {
                
                await IUserControl.Loading(Loading.ADD);
                if (data == null)
                    data = new Dictionary<string, object>();
                response = await ((UsersHelper)IHelper).Login(data);
                if (response == null ||
                    response.ContainsKey("Error") ||
                    !response.ContainsKey("User"))
                    return response;
                await Cancel();
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
                return response;
            }
            finally
            {
                await IUserControl.Loading(Loading.REMOVE); 
            }
        }

        public void LoginCommandExecute(object parameter) { Login(null); }

    }
}