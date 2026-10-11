using System;
using EduHub.Domain;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("==========================================================");
Console.WriteLine("=== EDUHUB - CONSOLE DEMO (8 BƯỚC THEO ĐÁP ÁN MẪU) ===");
Console.WriteLine("==========================================================");
Console.WriteLine();

try
{
    // BƯỚC 1: Tạo CLB
    Console.WriteLine("--- 1. Tạo CLB ---");
    var club = Club.Create("CLB IT EduHub", "Câu lạc bộ Công nghệ", 10);
    Console.WriteLine($"[+] Đã tạo thành công: {club.ClubName} (Sức chứa: {club.MaxMembers})");
    Console.WriteLine();

    // BƯỚC 2: Thêm 3 thành viên (Khởi tạo sinh viên)
    Console.WriteLine("--- 2. Thêm 3 thành viên ---");
    var sv1 = Student.Create("SV001", "Nguyễn Văn A", "a@eduhub.vn", "0901111111");
    var sv2 = Student.Create("SV002", "Trần Thị B", "b@eduhub.vn", "0902222222");
    var sv3 = Student.Create("SV003", "Lê Văn C", "c@eduhub.vn", "0903333333");
    Console.WriteLine($"[+] Đã khởi tạo 3 Sinh viên: {sv1.FullName}, {sv2.FullName}, {sv3.FullName}");
    Console.WriteLine();

    // BƯỚC 3: Tạo sự kiện
    Console.WriteLine("--- 3. Tạo sự kiện ---");
    var evt = club.CreateEvent("Tech Talk 2026", "Xu hướng .NET 10", DateTime.Now.AddDays(7), DateTime.Now.AddDays(5), 50);
    Console.WriteLine($"[+] Đã tạo sự kiện: {evt.EventName} (Số chỗ: {evt.MaxSeats})");
    Console.WriteLine();

    // BƯỚC 4: Đăng ký (Pending)
    Console.WriteLine("--- 4. Đăng ký (Pending) ---");
    var req1 = sv1.RegisterClub(club);
    var req2 = sv2.RegisterClub(club);
    var req3 = sv3.RegisterClub(club);
    Console.WriteLine($"[+] {sv1.FullName} gửi yêu cầu gia nhập. Trạng thái phiếu: {req1.Status}");
    Console.WriteLine($"[+] {sv2.FullName} gửi yêu cầu gia nhập. Trạng thái phiếu: {req2.Status}");
    Console.WriteLine();

    // BƯỚC 5: Xác nhận (Accepted)
    Console.WriteLine("--- 5. Xác nhận (Accepted) ---");
    req1.Approve(club);
    req2.Approve(club);
    Console.WriteLine($"[+] Đã duyệt phiếu của {sv1.FullName}. Trạng thái: {req1.Status}");
    Console.WriteLine($"[+] Đã duyệt phiếu của {sv2.FullName}. Trạng thái: {req2.Status}");
    Console.WriteLine($"[+] Số thành viên CLB hiện tại: {club.CurrentMembers}/{club.MaxMembers}");
    Console.WriteLine();

    // BƯỚC 6: Check-in qua QR
    Console.WriteLine("--- 6. Check-in qua QR ---");
    var ticket1 = sv1.RegisterEvent(evt);
    var ticket2 = sv2.RegisterEvent(evt);

    Console.WriteLine($"[+] {sv1.FullName} nhận vé QR: {ticket1.QRCode}");
    ticket1.CheckIn();
    Console.WriteLine($"[+] {sv1.FullName} quét mã check-in thành công. Trạng thái vé: {ticket1.CheckInStatus}");
    Console.WriteLine();

    // BƯỚC 7: Báo cáo tham dự
    Console.WriteLine("--- 7. Báo cáo tham dự ---");
    Console.WriteLine($"*** DANH SÁCH VÉ SỰ KIỆN: {evt.EventName} ***");
    Console.WriteLine($"- {sv1.FullName} | Mã vé: {ticket1.TicketCode} | Trạng thái: {ticket1.CheckInStatus}");
    Console.WriteLine($"- {sv2.FullName} | Mã vé: {ticket2.TicketCode} | Trạng thái: {ticket2.CheckInStatus}");
    Console.WriteLine($"=> Chỗ trống còn lại của sự kiện: {evt.AvailableSeats}");
    Console.WriteLine();

    // BƯỚC 8: Thử vi phạm luật nghiệp vụ (Theo slide Demo 2/2)
    Console.WriteLine("--- 8. Thử vi phạm luật nghiệp vụ ---");
    req3.Cancel();
    Console.WriteLine($"[!] Đã HỦY phiếu đăng ký của {sv3.FullName}. Trạng thái hiện tại: {req3.Status}");

    try
    {
        Console.WriteLine($"[!] Cố tình gọi hàm Approve(...) để ban chủ nhiệm duyệt phiếu đã hủy của {sv3.FullName}...");
        req3.Approve(club);
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"[BẮT LỖI NGHIỆP VỤ THÀNH CÔNG]: \"{ex.Message}\"");
    }
    Console.WriteLine();
}
catch (Exception ex)
{
    Console.WriteLine($"Lỗi hệ thống: {ex.Message}");
}