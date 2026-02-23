# Mower Management API (C#)

## Yêu cầu

- .NET 8 SDK
- PostgreSQL 14+

## Cấu hình kết nối PostgreSQL

1. Tạo database:

```bash
psql -U postgres -c "CREATE DATABASE mower_management;"
```

Hoặc trong pgAdmin / DBeaver: tạo database tên `mower_management`.

### Kết nối từ A5 SQL (app quản lý DB)

Trong A5 SQL, tạo connection mới với **PostgreSQL** và điền:

| Tham số   | Giá trị            |
|-----------|--------------------|
| **Host**  | `localhost`        |
| **Port**  | `5432`             |
| **Database** | `mower_management` |
| **Username** | `postgres`       |
| **Password** | (mật khẩu PostgreSQL của bạn) |

- Nếu PostgreSQL cài trên máy khác: thay `localhost` bằng IP hoặc tên máy đó.
- Đảm bảo PostgreSQL đang chạy và cho phép kết nối từ máy cài A5 SQL (cấu hình `pg_hba.conf` nếu kết nối từ xa).

2. Chỉnh connection string trong `appsettings.json` hoặc tạo `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=mower_management;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

Hoặc set biến môi trường (Windows PowerShell):

```powershell
$env:ConnectionStrings__DefaultConnection = "Host=localhost;Port=5432;Database=mower_management;Username=postgres;Password=postgres"
```

## EF Core migrations

Tạo migration lần đầu (sau khi thêm/sửa entity):

```bash
cd backend
dotnet ef migrations add InitialCreate --output-dir Data/Migrations
```

Cập nhật database:

```bash
dotnet ef database update
```

(Xem lại connection string trong `appsettings.json` trước khi chạy.)

## Chạy API

```bash
dotnet restore
dotnet run
```

- API: http://localhost:5000
- Swagger: http://localhost:5000/swagger
- Health (kiểm tra DB): http://localhost:5000/api/health

**Nếu không vào được http://localhost:5000:**

1. Chắc chắn đã chạy backend: trong thư mục `backend` gõ `dotnet run` và đợi đến khi thấy dòng dạng *"Now listening on: http://localhost:5000"*.
2. Nếu cổng 5000 bị chiếm: đổi trong `Program.cs` dòng `UseUrls("http://localhost:5000")` sang cổng khác (ví dụ `http://localhost:5050`), rồi mở link tương ứng.
