﻿// This file is Part of CalDavSynchronizer (http://outlookcaldavsynchronizer.sourceforge.net/)
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
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using CalDavSynchronizer.Implementation;
using CalDavSynchronizer.Ui;
using Microsoft.Office.Interop.Outlook;

namespace CalDavSynchronizer.Contracts
{
  public class EventMappingConfiguration : MappingConfigurationBase
  {
    public ReminderMapping MapReminder { get; set; }
    public bool MapAttendees { get; set; }
    public bool MapBody { get; set; }
    public string EventCategory { get; set; }
    public bool UseEventCategoryColorAndMapFromCalendarColor { get; set; }
    public OlCategoryColor EventCategoryColor { get; set; }

    [XmlIgnore]
    public bool UseEventCategoryAsFilter
    {
      get { return !string.IsNullOrEmpty (EventCategory); }
    }

    public EventMappingConfiguration ()
    {
      MapReminder = ReminderMapping.JustUpcoming;
      MapAttendees = true;
      MapBody = true;
      UseEventCategoryColorAndMapFromCalendarColor = false;
      EventCategoryColor = OlCategoryColor.olCategoryColorNone;
    }

    public override IConfigurationForm<MappingConfigurationBase> CreateConfigurationForm (IConfigurationFormFactory factory)
    {
      return factory.Create (this);
    }
  }
}