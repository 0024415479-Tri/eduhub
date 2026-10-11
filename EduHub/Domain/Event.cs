using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Domain;

public class Event
{
    public Guid EventID { get; private set; }
    public Guid ClubID { get; private set; }
    public string EventName { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime StartDate { get; private set; }
    public DateTime RegistrationDeadline { get; private set; }
    public int MaxSeats { get; private set; }
    public int AvailableSeats { get; private set; }
    public string Status { get; private set; } = "Scheduled";

    private Event(Guid clubID, string eventName, string description, DateTime startDate, DateTime deadline, int maxSeats)
    {
        EventID = Guid.NewGuid();
        ClubID = clubID;
        EventName = eventName;
        Description = description;
        StartDate = startDate;
        RegistrationDeadline = deadline;
        MaxSeats = maxSeats;
        AvailableSeats = maxSeats;
    }

    public static Event Create(Guid clubID, string eventName, string description, DateTime startDate, DateTime deadline, int maxSeats)
    {
        if (clubID == Guid.Empty)
            throw new ArgumentException("Mã câu lạc bộ không hợp lệ.", nameof(clubID));

        if (string.IsNullOrWhiteSpace(eventName))
            throw new ArgumentException("Tên sự kiện không được để trống.", nameof(eventName));

        if (startDate <= DateTime.Now)
            throw new ArgumentException("Ngày bắt đầu sự kiện phải diễn ra ở tương lai.", nameof(startDate));

        if (deadline >= startDate)
            throw new ArgumentException("Hạn đăng ký phải trước ngày diễn ra sự kiện.", nameof(deadline));

        if (maxSeats <= 0)
            throw new ArgumentException("Số lượng chỗ ngồi phải lớn hơn 0.", nameof(maxSeats));

        return new Event(clubID, eventName, description, startDate, deadline, maxSeats);
    }

    public bool HasAvailableSeats() => AvailableSeats > 0;

    public bool IsRegistrationOpen() => DateTime.Now <= RegistrationDeadline && HasAvailableSeats();

    public void BookSeat()
    {
        if (!HasAvailableSeats())
            throw new InvalidOperationException("Sự kiện đã hết chỗ.");
        AvailableSeats--;
    }
}