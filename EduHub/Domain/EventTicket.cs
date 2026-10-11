using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Domain;

public class EventTicket
{
    public Guid TicketID { get; private set; }
    public Guid EventID { get; private set; }
    public Guid StudentID { get; private set; }
    public string TicketCode { get; private set; } = string.Empty;
    public string QRCode { get; private set; } = string.Empty;
    public DateTime RegistrationDate { get; private set; }
    public string CheckInStatus { get; private set; } = "NotCheckedIn";

    private EventTicket(Guid eventID, Guid studentID)
    {
        TicketID = Guid.NewGuid();
        EventID = eventID;
        StudentID = studentID;
        TicketCode = $"TICKET-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        RegistrationDate = DateTime.Now;
        GenerateQRCode();
    }

    public static EventTicket Create(Guid eventID, Guid studentID)
    {
        if (eventID == Guid.Empty)
            throw new ArgumentException("Mã sự kiện không hợp lệ.", nameof(eventID));

        if (studentID == Guid.Empty)
            throw new ArgumentException("Mã sinh viên không hợp lệ.", nameof(studentID));

        return new EventTicket(eventID, studentID);
    }

    public void GenerateQRCode()
    {
        QRCode = $"QR_CODE_{TicketCode}";
    }

    public void CheckIn()
    {
        if (CheckInStatus == "CheckedIn")
            throw new InvalidOperationException("Vé này đã được check-in trước đó.");

        CheckInStatus = "CheckedIn";
    }
}