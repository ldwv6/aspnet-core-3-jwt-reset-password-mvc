# aspnet-core-3-jwt-reset-password-mvc

### ASP.NET Core 3.0 - IIS에 배포하는 방법 아래 링크에서 확인 

- http://www.csharpstudy.com/web/article/21--NET-Core-3-0---IIS-%EB%B0%B0%ED%8F%AC-%EB%B0%A9%EB%B2%95

### 이슈 사항 

- Reset 메일 발신 시 smtp 서버를 찾지 못함 >> 네임서버로 인한 문제
- app.UseDeveloperExceptionPage() 를 startup.cs 에 configure 메서드 안에 넣어 Debuging 가능 아래 내용 참조

https://stackoverflow.com/questions/55159025/error-an-error-occurred-while-processing-your-request-asp-net-core-mvc-2


### app.UseDeveloperExceptionPage() 추가 후 이슈 확인 예 

- DC 에 IIS 를 올려서 배포 할시 APP 기동 ID 변경해야 하는 이슈 아래 경로에 접근 불가로 인한 문제 

C:\Windows\System32\config\systemprofile\AppData\Local\ASP.NET\DataProtection-Keys
