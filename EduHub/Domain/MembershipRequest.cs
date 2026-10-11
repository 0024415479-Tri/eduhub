using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Domain;

public class MembershipRequest
{
    public Guid RequestID { get; private set; }
    public Guid StudentID { get; private set; }
    public Guid ClubID { get; private set; }
    public DateTime RequestDate { get; private set; }
    public string Status { get; private set; } = "Pending";
    public DateTime? ReviewedAt { get; private set; }

    private MembershipRequest(Guid studentID, Guid clubID)
    {
        RequestID = Guid.NewGuid();
        StudentID = studentID;
        ClubID = clubID;
        RequestDate = DateTime.Now;
    }

    public static MembershipRequest Create(Guid studentID, Guid clubID)
    {
        if (studentID == Guid.Empty)
            throw new ArgumentException("Mã sinh viên không hợp lệ.", nameof(studentID));

        if (clubID == Guid.Empty)
            throw new ArgumentException("Mã câu lạc bộ không hợp lệ.", nameof(clubID));

        return new MembershipRequest(studentID, clubID);
    }

    public void Submit() => Status = "Pending";

    public void Approve(Club club)
    {
        if (Status != "Pending")
            throw new InvalidOperationException("Yêu cầu đã bị hủy hoặc đã được xử lý trước đó.");

        Status = "Accepted";
        ReviewedAt = DateTime.Now;
        club.AddMember();
    }

    public void Cancel()
    {
        if (Status != "Pending")
            throw new InvalidOperationException("Chỉ có thể hủy yêu cầu đang ở trạng thái Chờ (Pending).");

        Status = "Cancelled";
        ReviewedAt = DateTime.Now;
    }
}