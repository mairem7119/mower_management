# Mower Management

Dự án quản lý thông tin thu thập từ máy cắt cỏ.

## Cấu trúc dự án

- **frontend/** – Ứng dụng React (Vite)
- **backend/** – API C# ASP.NET Core
- **Database** – PostgreSQL

## Yêu cầu

- Node.js 18+
- .NET 8 SDK
- PostgreSQL 14+

## Chạy dự án

### Backend (C# API)

```bash
cd backend
dotnet run
```

API chạy tại: https://localhost:7xxx hoặc http://localhost:5xxx (xem output khi chạy).

### Frontend (React)

```bash
cd frontend
npm install
npm run dev
```

### Cấu hình PostgreSQL

Tạo file `backend/appsettings.Development.json` (hoặc set biến môi trường) với connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=mower_management;Username=postgres;Password=your_password"
  }
}
```

Tạo database:

```sql
CREATE DATABASE mower_management;
```

### Kết nối A5 SQL (app quản lý DB)

Dùng cùng thông tin kết nối PostgreSQL: **Host** `localhost`, **Port** `5432`, **Database** `mower_management`, **User** `postgres`, **Password** (mật khẩu của bạn). Chi tiết xem `backend/README.md`.
