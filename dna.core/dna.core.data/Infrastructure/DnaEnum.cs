using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.data.Infrastructure
{
    public enum MenuType
    {
        Admin = 1,
        Client = 0
    }
    public enum AdvertisingType
    {
        Slider = 1,
        Popup = 0
    }
    public enum Status
    {
        InActive = 0,
        Active = 1
    }
    public enum ArticleStatus
    {
        UnConfirmed = 0,
        Confirmed = 1,
        Archive = 2,
        Draft = 3

    }
    public enum OperatingSystem
    {
        Android = 0,
        iOS = 1,
        WindowsPhone = 2,
        Browser = 3

    }
}
