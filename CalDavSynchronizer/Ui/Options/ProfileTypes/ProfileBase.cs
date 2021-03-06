// This file is Part of CalDavSynchronizer (http://outlookcaldavsynchronizer.sourceforge.net/)
// Copyright (c) 2015 Gerhard Zehetbauer
// Copyright (c) 2015 Alexander Nimmervoll
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using CalDavSynchronizer.Contracts;
using CalDavSynchronizer.Implementation;
using CalDavSynchronizer.Ui.Options.BulkOptions.ViewModels;
using CalDavSynchronizer.Ui.Options.Models;
using CalDavSynchronizer.Ui.Options.ViewModels;

namespace CalDavSynchronizer.Ui.Options.ProfileTypes
{
  public abstract class ProfileBase : IProfileType
  {
    protected readonly IOptionsViewModelParent OptionsViewModelParent;
    protected readonly IOutlookAccountPasswordProvider OutlookAccountPasswordProvider;
    protected readonly IReadOnlyList<string> AvailableCategories;
    protected readonly IOptionTasks OptionTasks;
    protected readonly ISettingsFaultFinder SettingsFaultFinder;
    protected readonly GeneralOptions GeneralOptions;
    protected readonly IViewOptions ViewOptions;

    protected ProfileBase(IOptionsViewModelParent optionsViewModelParent, IOutlookAccountPasswordProvider outlookAccountPasswordProvider, IReadOnlyList<string> availableCategories, IOptionTasks optionTasks, ISettingsFaultFinder settingsFaultFinder, GeneralOptions generalOptions, IViewOptions viewOptions)
    {
      if (optionsViewModelParent == null) throw new ArgumentNullException(nameof(optionsViewModelParent));
      if (outlookAccountPasswordProvider == null) throw new ArgumentNullException(nameof(outlookAccountPasswordProvider));
      if (availableCategories == null) throw new ArgumentNullException(nameof(availableCategories));
      if (optionTasks == null) throw new ArgumentNullException(nameof(optionTasks));
      if (settingsFaultFinder == null) throw new ArgumentNullException(nameof(settingsFaultFinder));
      if (generalOptions == null) throw new ArgumentNullException(nameof(generalOptions));
      if (viewOptions == null) throw new ArgumentNullException(nameof(viewOptions));

      OptionsViewModelParent = optionsViewModelParent;
      OutlookAccountPasswordProvider = outlookAccountPasswordProvider;
      AvailableCategories = availableCategories;
      OptionTasks = optionTasks;
      SettingsFaultFinder = settingsFaultFinder;
      GeneralOptions = generalOptions;
      ViewOptions = viewOptions;
    }

    public abstract string Name { get; }

    public OptionsModel CreateNewModel()
    {
      var options = CreateData();
      InitializeData(options);
      return CreateModel(options);
    }

    public OptionsModel CreateModelFromData(Contracts.Options data)
    {
      return CreateModel(data);
    }

    protected virtual OptionsModel CreateModel(Contracts.Options data)
    {
      return new OptionsModel(SettingsFaultFinder, OptionTasks, OutlookAccountPasswordProvider, data, GeneralOptions, this, false);
    }

    protected virtual void InitializeData(Contracts.Options data)
    {

    }

    private Contracts.Options CreateData()
    {
      return new Contracts.Options
      {
        ConflictResolution = ConflictResolution.Automatic,
        DaysToSynchronizeInTheFuture = 365,
        DaysToSynchronizeInThePast = 60,
        SynchronizationIntervalInMinutes = 30,
        SynchronizationMode = SynchronizationMode.MergeInBothDirections,
        Name = "<New Profile>",
        Id = Guid.NewGuid(),
        Inactive = false,
        PreemptiveAuthentication = true,
        ForceBasicAuthentication = false,
        ProxyOptions = new ProxyOptions() { ProxyUseDefault = true },
        IsChunkedSynchronizationEnabled = false,
        ChunkSize = 100,
        ServerAdapterType = ServerAdapterType.WebDavHttpClientBased
      };
    }
  
    protected virtual OptionsModel CreatePrototypeModel(Contracts.Options data)
    {
      return new OptionsModel(SettingsFaultFinder, OptionTasks, OutlookAccountPasswordProvider, data, GeneralOptions, this, false);
    }

    protected virtual void InitializePrototypeData(Contracts.Options data)
    {

    }

    public virtual IOptionsViewModel CreateViewModel(OptionsModel model)
    {
      var optionsViewModel = new GenericOptionsViewModel(
        OptionsViewModelParent,
        new ServerSettingsViewModel(model, OptionTasks, ViewOptions),
        OptionTasks,
        model,
        AvailableCategories,
        ViewOptions);

      return optionsViewModel;
    }

    public IOptionsViewModel CreateTemplateViewModel()
    {
      var data = CreateData();
      data.Name = Name;
      InitializePrototypeData(data);
      var prototypeModel = CreateModel(data);
      var optionsViewModel = CreateTemplateViewModel(prototypeModel);

      return optionsViewModel;
    }

    protected virtual IOptionsViewModel CreateTemplateViewModel(OptionsModel prototypeModel)
    {
      var optionsViewModel = new MultipleOptionsTemplateViewModel(
        OptionsViewModelParent,
        new ServerSettingsTemplateViewModel(OutlookAccountPasswordProvider, prototypeModel),
        OptionTasks,
        prototypeModel,
        ViewOptions);
      return optionsViewModel;
    }
  }
}