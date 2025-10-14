[web] GET /api/reports/{id:guid}/summary  (Sample.Web.Controllers.ReportsController.GetReportSummary)  [L37–L47]
  └─ uses_client ReportsClient [L40]
    └─ calls GetDetails (GET /api/v2/reports/{*}/details, base=https://localhost:6001, config=Downstream:Reports:BaseUrl, target=ReportsApi) [L30]
      └─ target_service ReportsApi
        └─ [web] GET /api/reports/{id:guid}  (Sample.ReportsApi.Endpoints.MapGet)  [L26–L26]
        └─ [web] GET /api/v2/reports/{id:guid}/details  (Sample.ReportsApi.Controllers.ReportsController.GetDetails)  [L10–L15]
    └─ calls MapGet (GET /api/reports/{*}, base=https://localhost:6001, config=Downstream:Reports:BaseUrl, target=ReportsApi) [L17]
      └─ target_service ReportsApi
        └─ [web] GET /api/reports/{id:guid}  (Sample.ReportsApi.Endpoints.MapGet)  [L26–L26]
        └─ [web] GET /api/v2/reports/{id:guid}/details  (Sample.ReportsApi.Controllers.ReportsController.GetDetails)  [L10–L15]
    └─ calls MapPost (POST /api/reports/search, base=https://localhost:6001, config=Downstream:Reports:BaseUrl, target=ReportsApi) [L22]
      └─ target_service ReportsApi
        └─ [web] POST /api/reports/search  (Sample.ReportsApi.Endpoints.MapPost)  [L32–L32]

