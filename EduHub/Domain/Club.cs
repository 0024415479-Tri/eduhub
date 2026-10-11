using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Domain;

public class Club
{
    public Guid ClubID { get; private set; }
    public string ClubName { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int MaxMembers { get; private set; }
    public int CurrentMembers { get; private set; }
    public string Status { get; private set; } = "Active";
    public bool RecruitmentOpen { get; private set; }

    private Club(string clubName, string description, int maxMembers)
    {
        ClubID = Guid.NewGuid();
        ClubName = clubName;
        Description = description;
        MaxMembers = maxMembers;
        CurrentMembers = 0;
        RecruitmentOpen = true;
    }

    public static Club Create(string clubName, string description, int maxMembers)
    {
        if (string.IsNullOrWhiteSpace(clubName))
            throw new ArgumentException("Tên câu lạc bộ không được để trống.", nameof(clubName));

        if (maxMembers <= 0)
            throw new ArgumentException("Số lượng thành viên tối đa phải lớn hơn 0.", nameof(maxMembers));

        return new Club(clubName, description, maxMembers);
    }

    public bool CanRecruit()
        => Status == "Active" && RecruitmentOpen && CurrentMembers < MaxMembers;

    public void OpenRecruitment() => RecruitmentOpen = true;

    public void Suspend() => Status = "Suspended";

    public void AddMember()
    {
        if (CurrentMembers >= MaxMembers)
            throw new InvalidOperationException("Câu lạc bộ đã đạt số lượng thành viên tối đa.");
        CurrentMembers++;
    }

    public Event CreateEvent(string eventName, string description, DateTime startDate, DateTime deadline, int maxSeats)
    {
        if (Status == "Suspended")
            throw new InvalidOperationException("Câu lạc bộ đang bị tạm khóa, không thể tạo sự kiện.");

        return Event.Create(ClubID, eventName, description, startDate, deadline, maxSeats);
    }
}