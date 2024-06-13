# Kế thừa Image dùng để tạo môi trường runtime cho .net core --> đóng vai trò là một Image Base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# Thư mục làm việc bên trong thư mục Image 
WORKDIR /app 
# Cổng mà Container chạy Image sẽ mở ra cho bên ngoài trỏ vào
EXPOSE 80 

# Kế thừa Image dùng để build core ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# Thư mục làm việc để build bên trong thư mục Image
WORKDIR /src 
# Copy các file .csproj của các Solution vào các thư mục tương ứng trong Image 
COPY ["PharmacyManagement_BE.API/PharmacyManagement_BE.API.csproj", "PharmacyManagement_BE.API/"]
COPY ["PharmacyManagement_BE.Application/PharmacyManagement_BE.Application.csproj", "PharmacyManagement_BE.Application/"]
COPY ["PharmacyManagement_BE.Infrastructure/PharmacyManagement_BE.Infrastructure.csproj", "PharmacyManagement_BE.Infrastructure/"]
COPY ["PharmacyManagement_BE.Domain/PharmacyManagement_BE.Domain.csproj", "PharmacyManagement_BE.Domain/"]
# Sau khi copy xong, nó sẽ run lệnh dotnet restore của .NET Core để download các package từ Nuget về phục vụ việc build.
RUN dotnet restore "PharmacyManagement_BE.API/PharmacyManagement_BE.API.csproj"
# Copy toàn bộ file ở thư mục hiện tại sang thư mục src.
COPY . .
# Chuyển thư mục làm việc vào trong /src/PharmacyManagement_BE.API
WORKDIR "/src/PharmacyManagement_BE.API"
#  Chạy lệnh build project PharmacyManagement_BE.API.csproj với mode Release và output ra thư mục app của image.
RUN dotnet build "PharmacyManagement_BE.API.csproj" -c Release -o /app/build

# Kế thừa Image alias publish từ image build lúc trước.
FROM build AS publish
# Publish project PharmacyManagement_BE.API.csproj và output ra thư mục app.
RUN dotnet publish "PharmacyManagement_BE.API.csproj" -c Release -o /app/publish

FROM base AS final
# Chuyển thư mục àm việc về /app
WORKDIR /app
# Copy toàn bộ image publish sang thư mục /app
COPY --from=publish /app/publish .
#ạo Entry point truy cập ứng dụng từ PharmacyManagement_BE.API.dll
ENTRYPOINT ["dotnet", "PharmacyManagement_BE.API.dll"]
