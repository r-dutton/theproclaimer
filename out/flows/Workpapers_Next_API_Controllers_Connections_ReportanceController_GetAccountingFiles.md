[web] GET /api/connections/reportance/files  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetAccountingFiles)  [L28–L34] status=200
  └─ sends_request GetFilesQuery [L31]
    └─ handled_by Workpapers.Next.CirrusServices.Queries.GetFilesQueryHandler.Handle [L12–L39]
      └─ uses_service CirrusConfig
        └─ method GetBaseUrl [L27]
          └─ ... (no implementation details available)
      └─ uses_service CirrusHttp
        └─ method GetHttpResponseAsync [L35]
          └─ ... (no implementation details available)
      └─ uses_service IMapToNew<FileSearchResult, AccountingFileDto>
        └─ method Map [L37]
          └─ ... (no implementation details available)

