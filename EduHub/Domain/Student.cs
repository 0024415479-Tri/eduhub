using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Domain;

public class Student
{
    public Guid StudentID { get; private set; }
    public string StudentCode { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;

    private Student(string studentCode, string fullName, string email, string phone)
    {
        StudentID = Guid.NewGuid();
        StudentCode = studentCode;
        FullName = fullName;
        Email = email;
        Phone = phone;
    }

    public static Student Create(string studentCode, string fullName, string email, string phone)
    {
        if (string.IsNullOrWhiteSpace(studentCode))
            throw new ArgumentException("Mã sinh viên không được để trống.", nameof(studentCode));

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Họ tên sinh viên không được để trống.", nameof(fullName));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("Email không hợp lệ.", nameof(email));

        return new Student(studentCode, fullName, email, phone);
    }

    public MembershipRequest RegisterClub(Club club)
    {
        if (!club.CanRecruit())
            throw new InvalidOperationException("Câu lạc bộ hiện không mở tuyển hoặc đã đủ số lượng thành viên.");

        var request = MembershipRequest.Create(StudentID, club.ClubID);
        request.Submit();
        return request;
    }

    public EventTicket RegisterEvent(Event evt)
    {
        if (!evt.IsRegistrationOpen())
            throw new InvalidOperationException("Sự kiện đã hết chỗ hoặc đã hết hạn đăng ký.");

        evt.BookSeat();
        return EventTicket.Create(evt.EventID, StudentID);
    }
}