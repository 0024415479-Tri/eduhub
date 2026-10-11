# 1 · User Stories

## US-01 · Sinh viên: Đăng ký tham gia Câu lạc bộ

**1. CLB đang mở đợt tuyển + Sinh viên chưa từng gia nhập -> Nhấn đăng ký -> Tạo yêu cầu gia nhập (trạng thái Pending).**
**2. CLB đã đủ số lượng thành viên tối đa hoặc Sinh viên đã là thành viên -> Nhấn đăng ký -> Báo lỗi không thể đăng ký.**

## US-02 · Sinh viên: Đăng ký tham gia Sự kiện

**1. Sự kiện còn chỗ (AvailableSeats > 0) và chưa hết hạn đăng ký -> Nhấn đăng ký -> Tạo vé tham dự và cấp mã QR check-in.**
**2. Sự kiện đã hết chỗ (AvailableSeats = 0) -> Nhấn đăng ký -> Báo lỗi sự kiện đã đủ số lượng.**

## US-03 · Ban chủ nhiệm: Tạo sự kiện mới cho CLB

**1. Tên sự kiện chưa trùng + Ngày bắt đầu sau thời điểm hiện tại + Số lượng chỗ > 0 -> Nhấn tạo -> Lưu thông tin sự kiện vào CLB.**
**2. Ngày bắt đầu ở quá khứ hoặc số lượng chỗ <= 0 -> Nhấn tạo -> Báo lỗi thông tin sự kiện không hợp lệ.**

## US-04 · Ban chủ nhiệm: Duyệt thành viên mới vào CLB

**1. Yêu cầu đang ở trạng thái Pending -> Nhấn duyệt -> Cập nhật trạng thái thành Accepted và tăng số lượng thành viên CLB thêm 1.**
**2. Yêu cầu đã bị hủy hoặc không tồn tại -> Nhấn duyệt -> Báo lỗi yêu cầu không hợp lệ.**

## US-05 · Admin: Tạm khóa Câu lạc bộ vi phạm quy định

**1. CLB vi phạm nội quy trường -> Nhấn khóa -> Cập nhật Club.Status = Suspended (ngăn tạo sự kiện và tuyển thành viên mới).**
**2. Lịch sử hoạt động và danh sách thành viên cũ của CLB đó -> Vẫn được lưu trữ đầy đủ trong cơ sở dữ liệu.**
