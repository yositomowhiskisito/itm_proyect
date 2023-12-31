﻿using LIBDomainEntities.Entities;
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
        public VmPersons(Dictionary<string, object> data) : base(data)
        {
            try
            {
                IHelper = new PersonsHelper();
                FileCommand = new EventsCommand(FileCommandExecute);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public EventsCommand FileCommand { get; private set; }

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

        public void FileCommandExecute(object parameter) { File(null); }
    }
}