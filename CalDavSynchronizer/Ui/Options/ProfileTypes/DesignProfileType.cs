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

using CalDavSynchronizer.Ui.Options.Models;
using CalDavSynchronizer.Ui.Options.ViewModels;

namespace CalDavSynchronizer.Ui.Options.ProfileTypes
{
  class DesignProfileType : IProfileType
  {
    public static IProfileType Instance => new DesignProfileType();

    public string Name { get; } = "Design Profile";
    public OptionsModel CreateNewModel()
    {
      return OptionsModel.DesignInstance;
    }

    public OptionsModel CreateModelFromData(Contracts.Options data)
    {
      return OptionsModel.DesignInstance;
    }

    public IOptionsViewModel CreateViewModel(OptionsModel model)
    {
      return GenericOptionsViewModel.DesignInstance;
    }

    public IOptionsViewModel CreateTemplateViewModel()
    {
      return GenericOptionsViewModel.DesignInstance;
    }
  }
}