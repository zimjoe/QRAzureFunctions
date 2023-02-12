using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator.ContactData;
using static QRCoder.PayloadGenerator;

namespace Aeveco.AzureFunction.Application.Models;

public class ContactQRRequest
{
    /// <summary>
    /// ContactData.ContactOutputType
    /// Supported formats are: vCard 2.1, vCard 3.0, vCard 4.0 and MeCard. 
    /// Choose the format which fits your needs. Right now vCard 3.0 is the most used format.
    /// </summary>
    public ContactOutputType ContactOutput { get; set; } = ContactOutputType.VCard3;

    /// <summary>
    /// AddressOrder
    /// The addressOrder enum specifies in which format the address is rendered. The two options are:
    /// Default - The address format used in European countries and others.
    /// Reversed - The address format used in North America and others.
    /// </summary>
    public AddressOrder AddressOrderType { get; set; } = AddressOrder.Default;

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? NickName { get; set; }
    public string? Phone { get; set; }
    public string? MobilePhone { get; set; }
    public string? WorkPhone { get; set; }
    public string? Email { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Website { get; set; }

    public string? HouseNumber { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? StateRegion { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }

    

    public string? Note { get; set; }

    public string? Organization { get; set; }
    public string? Title { get; set; }

}

